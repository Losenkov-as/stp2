insert into "Machines"  ("Id","InventoryNumber") values (1, '001');
insert into "Machines"  ("Id","InventoryNumber") values (2, '002');

insert into "Locations"  ("Id","Room","Build") values (1, '111', '111');
insert into "Locations"  ("Id","Room","Build") values (2, '112', '112');

insert into "Statuses"  ("Id","Name") values (1,'Работает');
insert into "Statuses"  ("Id","Name") values (2, 'Отказ');
insert into "Statuses"  ("Id","Name") values (3, 'Ремонт');
insert into "Statuses"  ("Id","Name") values (4, 'Приостановлен');

insert into "Maintenances"  ("Id","DateOfUpdate", "MachineId", "StatusId", "LocationId", "UserId") 
	values (1, now(), 1, 4, 1, 2);
insert into "Maintenances"  ("Id","DateOfUpdate", "MachineId", "StatusId", "LocationId", "UserId") 
	values (2, now(), 2, 1, 2, 2);
