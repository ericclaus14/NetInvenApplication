USE NetInventory

DELETE FROM virtual_machines;
DELETE FROM computers;
DELETE FROM wired_devices;
DELETE FROM network_devices;
DELETE FROM devices;
DELETE FROM device_types;
DELETE FROM vlans;

INSERT INTO vlans VALUES('100','Management VLAN');
INSERT INTO vlans VALUES('200','Server VLAN');
INSERT INTO vlans VALUES('300','User VLAN');
INSERT INTO vlans VALUES('400','VOIP VLAN');
INSERT INTO vlans VALUES('500','Printer VLAN');

INSERT INTO device_types VALUES ('Computer','Laptop');
INSERT INTO device_types VALUES ('Computer','Desktop');
INSERT INTO device_types VALUES ('Printer','B&W');
INSERT INTO device_types VALUES ('Printer','Color');
INSERT INTO device_types VALUES ('Printer','MFP');
INSERT INTO device_types VALUES ('Phone','VOIP');
INSERT INTO device_types VALUES ('Phone','Cell');
INSERT INTO device_types VALUES ('Mobile','Tablet');
INSERT INTO device_types VALUES ('Projection','Projector');
INSERT INTO device_types VALUES ('Projection','DVD/VHS');
INSERT INTO device_types VALUES ('Server','Hypervisor');
INSERT INTO device_types VALUES ('Server','DC');
INSERT INTO device_types VALUES ('Server','App');
INSERT INTO device_types VALUES ('Server','Services');
INSERT INTO device_types VALUES ('Network','Switch');
INSERT INTO device_types VALUES ('Network','Firewall');
INSERT INTO device_types VALUES ('Network','AP');
INSERT INTO device_types VALUES ('Other','Other');

INSERT INTO devices VALUES('1001','Core Switch','Network','Switch','HP','Procurve 5412','J968DF98','Building 2','900','Active');
INSERT INTO devices VALUES('1002','Edge Switch','Network','Switch','HP','Procurve 5412','J968DF90','Building 1','900','Active');
INSERT INTO devices VALUES('1003','Outside Firewall','Network','Firewall','Fortinet','Fortigate 5000','FTG89123','Building 2','900','Active');
INSERT INTO devices	VALUES('2001','Primary Hypervisor','Server','Hypervisor','Dell','Poweredge R400','GKJ89DH','Building 2','900','Active');
INSERT INTO devices VALUES('2002','Broken server','Server','Services','Dell','Poweredge R400','KJDIOS89','Building 2','900','Parts');
INSERT INTO devices VALUES('3001','John Doe Desktop','Computer','Desktop','Dell','Optiplex 9020','83490JQP','Building 1','100','Active');
INSERT INTO devices VALUES('3002','Jane Doe Laptop','Computer','Laptop','Dell','Latitude E6550','D8G0O8U8','Building 2','200','Active');
INSERT INTO devices VALUES('3003','Eric Claus Desktop','Computer','Desktop','Dell','Optiplex 9020','QN84JF90','Building 2','400','Active');
INSERT INTO devices VALUES('4001','John Doe VOIP','Phone','VOIP','Cisco','IP Phone 9260','CIP90349','Building 1','100','Active');
INSERT INTO devices VALUES('5001','Spare Printer','Printer','B&W','HP','LaserJet P3015n','NFW889900','Building 2','800','Available');
INSERT INTO devices VALUES('6001','Building 1 Projector','Projection','Projector','Epson','PowerLite 180','89347DUI','Building 1','500','Active');

INSERT INTO network_devices VALUES('1001','coreswitch1','AD:DF:12:31:41:BB','10.1.0.1');
INSERT INTO network_devices VALUES('1002','edgeswitch1','AD:DF:12:31:41:CC','10.1.0.2');
INSERT INTO network_devices VALUES('1003','firewall1','AD:DF:00:98:FF:DD','10.1.0.3');
INSERT INTO network_devices VALUES('2001','hyperv1','AB:CD:EF:12:34:56','10.2.0.1');
INSERT INTO network_devices VALUES('3001','johndoe-d','65:43:21:FE:DC:BA',NULL);
INSERT INTO network_devices VALUES('3002','janedoe-l','AA:BB:CC:11:22:33',NULL);
INSERT INTO network_devices VALUES('3003','ericclaus-d','DD:EE:FF:44:55:66','10.3.0.100');

INSERT INTO wired_devices VALUES('1001',NULL,NULL,'100');
INSERT INTO wired_devices VALUES('1002','coreswitch1','A2','100');
INSERT INTO wired_devices VALUES('1003','coreswitch1','A1','100');
INSERT INTO wired_devices VALUES('2001','coreswitch1','B1','200');
INSERT INTO wired_devices VALUES('3001','edgeswitch1','C1','300');
INSERT INTO wired_devices VALUES('3003','coreswitch1','C2','300');

INSERT INTO computers VALUES('2001','Svr 14 Datacenter','4x Xeon','128','2048');
INSERT INTO computers VALUES('2002',NULL,'4x Xeon','96','1024');
INSERT INTO computers VALUES('3001','Win 10 Ent','i7','8','256');
INSERT INTO computers VALUES('3002','Win 10 Ent','i5','8','256');
INSERT INTO computers VALUES('3003','Win 10 Ent','i7','16','512');

INSERT INTO virtual_machines VALUES('dc1','hyperv1','AD, DNS','Svr 16 Datacenter','4','8','80');
INSERT INTO virtual_machines VALUES('dc2','hyperv1','AD, DNS','Svr 16 Datacenter','4','8','80');