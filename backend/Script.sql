insert into "Machines"  ("Id","InventoryNumber") values (1, '001');
insert into "Machines"  ("Id","InventoryNumber") values (2, '002');

insert into "Locations"  ("Id","Room","Build", "UserId") values (1, '111', '111', 2);
insert into "Locations"  ("Id","Room","Build", "UserId") values (2, '112', '112', 2);

insert into "Statuses"  ("Id","Name") values (1,'��������');
insert into "Statuses"  ("Id","Name") values (2, '�����');
insert into "Statuses"  ("Id","Name") values (3, '������');
insert into "Statuses"  ("Id","Name") values (4, '�������������');

insert into "Maintenances"  ("Id","DateOfUpdate", "MachineId", "StatusId", "LocationId", "UserId") 
	values (1, now(), 1, 4, 1, 2);
insert into "Maintenances"  ("Id","DateOfUpdate", "MachineId", "StatusId", "LocationId", "UserId") 
	values (2, now(), 2, 1, 2, 2);
