DROP DATABASE NetInventory;
CREATE DATABASE NetInventory;
GO

USE NetInventory

CREATE TABLE vlans (
	vlan_id			smallint,
	name			varchar(30),
	PRIMARY KEY (vlan_id)
	);

CREATE TABLE device_types (
	type			varchar(15),
	subtype			varchar(15),
	PRIMARY KEY	(type, subtype)
	);

CREATE TABLE devices (
	asset_id		smallint, --IDENTITY(1000,1),
	description		varchar(30),
	device_type		varchar(15),
	device_subtype	varchar(15),
	manufacturer	varchar(15),
	model			varchar(30),
	serial_number	varchar(30) UNIQUE,
	building		varchar(30),
	room			varchar(15),
	status			varchar(9) CHECK (status IN ('Active','Available','Parts','Retired')),
	PRIMARY KEY (asset_id),
	FOREIGN KEY (device_type,device_subtype) REFERENCES device_types(type,subtype)
		ON DELETE SET NULL,
	);
	
CREATE TABLE network_devices (
	asset_id		smallint,
	host_name		varchar(30),
	mac_address		varchar(17),
	static_ip		varchar(15),
	PRIMARY KEY	(asset_id),
	FOREIGN KEY (asset_id) REFERENCES devices
		ON DELETE CASCADE
	);

CREATE TABLE wired_devices (
	asset_id		smallint,
	switch_name		varchar(30),
	switch_port		varchar(30),
	vlan_id			smallint,
	PRIMARY KEY (asset_id),
	FOREIGN KEY (asset_id) REFERENCES devices
		ON DELETE CASCADE,
	FOREIGN KEY (vlan_id) REFERENCES vlans
	);

CREATE TABLE computers (
	asset_id		smallint,
	os_version		varchar(30),
	cpu				varchar(30),
	ram				varchar(10),
	hdd				varchar(10),
	PRIMARY KEY	(asset_id),
	FOREIGN KEY (asset_id) REFERENCES devices
		ON DELETE CASCADE
	);

CREATE TABLE virtual_machines (
	name			varchar(30),
	hypervisor		varchar(30),
	services		varchar(30),
	os_version		varchar(30),
	vcpu			varchar(5),
	ram				varchar(5),
	hdd				varchar(5),
	PRIMARY KEY (name),
	);

GO
CREATE VIEW All_Device_Data
AS 
	SELECT d.*, n.host_name, n.mac_address, n.static_ip, w.switch_name, w.switch_port, w.vlan_id, c.os_version, c.cpu, c.ram, c.hdd
	FROM devices d
		LEFT JOIN network_devices n ON d.asset_id=n.asset_id
		LEFT JOIN wired_devices w ON d.asset_id=w.asset_id
		LEFT JOIN computers c ON d.asset_id=c.asset_id;
GO
CREATE VIEW Computers_View
AS
	SELECT d.*, n.host_name, n.mac_address, n.static_ip, w.switch_name, w.switch_port, w.vlan_id, c.os_version, c.cpu, c.ram, c.hdd
	FROM devices d
		LEFT JOIN network_devices n ON d.asset_id=n.asset_id
		LEFT JOIN wired_devices w ON d.asset_id=w.asset_id
		LEFT JOIN computers c ON d.asset_id=c.asset_id
	WHERE d.device_type='Computer';
GO
CREATE VIEW Servers_View 
AS
	SELECT d.*, n.host_name, n.mac_address, n.static_ip, w.switch_name, w.switch_port, w.vlan_id, c.os_version, c.cpu, c.ram, c.hdd
	FROM devices d
		LEFT JOIN network_devices n ON d.asset_id=n.asset_id
		LEFT JOIN wired_devices w ON d.asset_id=w.asset_id
		LEFT JOIN computers c ON d.asset_id=c.asset_id
	WHERE d.device_type='Server';
GO
CREATE VIEW Wired_Devices_View
AS
	SELECT d.*, n.host_name, n.mac_address, n.static_ip, w.switch_name, w.switch_port, w.vlan_id
	FROM wired_devices w
		LEFT JOIN devices d ON w.asset_id=d.asset_id
		LEFT JOIN network_devices n ON w.asset_id=n.asset_id;
GO
CREATE VIEW Switch_Maps
AS
SELECT w. switch_name, w.switch_port, w.vlan_id, d.asset_id, d.description, n.mac_address, n.static_ip, d. building, d.room
FROM devices d
	LEFT JOIN network_devices n ON d.asset_id=n.asset_id
	LEFT JOIN wired_devices w ON d.asset_id=w.asset_id
WHERE w.switch_name IN (
	SELECT n.host_name
	FROM devices d
		JOIN network_devices n ON d.asset_id=n.asset_id
	WHERE d.device_subtype='Switch')
GO
CREATE VIEW Device_Types_View
AS
SELECT DISTINCT type
FROM dbo.device_types
GO
CREATE TRIGGER Trig_INS_All_Device_Data ON  All_Device_Data 
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	-- Check for duplicate asset. If no there is duplicate, do an INSERT.
	IF (NOT EXISTS (SELECT d.asset_id
		  FROM devices d, inserted
		  WHERE d.asset_id = inserted.asset_id))
	   INSERT INTO devices
		  SELECT asset_id, description, device_type, device_subtype, manufacturer, model, 
			serial_number, building, room, status
		  FROM inserted
		INSERT INTO network_devices
			SELECT asset_id, host_name, mac_address, static_ip
			FROM inserted
		INSERT INTO wired_devices
			SELECT asset_id, switch_name, switch_port, vlan_id
			FROM inserted
		INSERT INTO computers
			SELECT asset_id, os_version, cpu, ram, hdd
			FROM inserted
END
GO
CREATE TRIGGER Trig_Upd_All_Device_Data on All_Device_Data
	INSTEAD OF UPDATE
AS
BEGIN
	UPDATE devices
		SET asset_id = i.asset_id,
			description = i.description,
			device_type = i.device_type,
			device_subtype = i.device_subtype,
			manufacturer = i.manufacturer,
			model = i.model,
			serial_number = i.serial_number,
			building = i.building,
			room = i.room,
			status = i.status
		FROM devices d, inserted i
		WHERE d.asset_id = i.asset_id
	
	IF (NOT EXISTS (SELECT n.asset_id
		FROM network_devices n, inserted i
		WHERE n.asset_id = i.asset_id))
		INSERT INTO network_devices
			SELECT asset_id, host_name, mac_address, static_ip
			FROM inserted
	ELSE
		UPDATE network_devices
			SET asset_id = i.asset_id,
				host_name = i.host_name,
				mac_address = i.mac_address,
				static_ip = i.static_ip
			FROM network_devices n, inserted i
			WHERE n.asset_id = i.asset_id
	
	IF (NOT EXISTS (SELECT w.asset_id
		FROM wired_devices w, inserted i
		WHERE w.asset_id = i.asset_id))
		INSERT INTO wired_devices
			SELECT asset_id, switch_name, switch_port, vlan_id
			FROM inserted
	ELSE
		UPDATE wired_devices
			SET asset_id = i.asset_id,
				switch_name = i.switch_name,
				switch_port = i.switch_port,
				vlan_id = i.vlan_id
			FROM wired_devices w, inserted i
			WHERE w.asset_id = i.asset_id
	IF (NOT EXISTS (SELECT c.asset_id
		FROM computers c, inserted i
		WHERE c.asset_id = i.asset_id))
		INSERT INTO computers
			SELECT asset_id, os_version, cpu, ram, hdd
			FROM inserted
	ELSE
		UPDATE computers
			SET asset_id = i.asset_id,
				os_version = i.os_version,
				cpu = i.cpu,
				ram = i.ram,
				hdd = i.hdd
			FROM computers c, inserted i
			WHERE c.asset_id = i.asset_id
END
GO
CREATE TRIGGER Trig_Dlt_All_Device_Data on All_Device_Data
	INSTEAD OF DELETE
AS
BEGIN
	DECLARE @asset_id smallint
	SELECT @asset_id = d.asset_id
	FROM deleted d

	DELETE FROM network_devices
	WHERE asset_id = @asset_id

	DELETE FROM wired_devices
	WHERE asset_id = @asset_id

	DELETE FROM computers
	WHERE asset_id = @asset_id

	DELETE FROM devices
	WHERE asset_id = @asset_id
END