DELIMITER 
|
drop procedure if exists SuperCadre
|
create procedure SuperCadre()
Begin
	drop view if exists cadre;
	create view cadre as 
	select * from employe 
	where emp_cadre = 1;
	select emp_nom 
	from cadre 
	where emp_salaire > (select AVG(emp_salaire)from cadre);
End
|
Call SuperCadre()
|


DELIMITER |
drop procedure if exists MSalariale|
create procedure MSalariale(service varchar(50), out masse decimal)
Begin 
	select sum(emp_salaire) into masse
	from employe e inner join service s on e.emp_service = s.ser_id
	where ser_designation = service;
End
|
call MSalariale("Atelier A", @masse)
|
select @masse
|



DELIMITER |
drop procedure if exists majSalaire|
create procedure majSalaire(nom varchar(50),  pourcent int(2))
Begin 
	update employe
	set emp_salaire = emp_salaire * (1+pourcent/100)
	where emp_nom = nom;
End
|
call majSalaire("SAPKIR", 20)
|

//////////////////////////////////////////////////////////////////////////

DELIMITER |
drop procedure if exists Moyenne|
create procedure Moyenne()
Begin
	drop view if exists qtéDiplome ;
	create view qtéDiplome (nomEmp,numEmp,Nbdip) as 
	select emp_nom,pos_employe,count(distinct pos_diplome) from employe e inner join posseder p on e.emp_id=p.pos_employe inner join diplome d on p.pos_diplome=d.dip_id group by pos_employe;
	select nomEmp,numEmp from qtéDiplome where Nbdip>(select avg(Nbdip) from qtéDiplome);
End|
Call Moyenne()
|

delimiter |
DROP PROCEDURE IF EXISTS Budget|

CREATE PROCEDURE Budget(value decimal(10,2), numID int)
BEGIN
	update service 
	set ser_budget = ser_budget * value
	where ser_type = 'A' and ser_id = numId;
END
|
CALL Budget(1.4, 2)
|

delimiter |
DROP PROCEDURE IF EXISTS borneSalaire |

CREATE PROCEDURE borneSalaire(borneInferieur int, borneSuperieur int)
BEGIN
	select emp_nom, emp_prenom from employe where emp_salaire >= borneInferieur and emp_salaire <= borneSuperieur;
END
|
CALL borneSalaire(1500, 3500)
|

delimiter |
DROP PROCEDURE IF EXISTS selectDip |

CREATE PROCEDURE selectDip()
BEGIN
	select emp_nom,emp_prenom from employe e INNER JOIN posseder p 
		ON e.emp_id = p.pos_employe INNER JOIN diplome d 
		ON p.pos_diplome = d.dip_id and dip_libelle="Bac" and emp_id 
	in(select emp_id from employe e INNER JOIN posseder p 
		ON e.emp_id = p.pos_employe INNER JOIN diplome d 
		ON p.pos_diplome = d.dip_id where dip_libelle="Licence");
END
|
CALL selectDip()
|


delimiter |
DROP PROCEDURE IF EXISTS newEmploye |

CREATE PROCEDURE newEmploye (nom varchar(50), prenom varchar(50), sexe varchar(1), cadre bit, salaire decimal(10,2), service int)
BEGIN
	insert into employe(emp_nom, emp_prenom, emp_sexe, emp_cadre, emp_salaire, emp_service) values(nom, prenom, sexe, cadre, salaire, service);
END
|
CALL newEmploye("mmadi", "nass", "M", 1, 15000, 3)
|