-- ConfigItem.sql
-- Configuration values.
DROP TABLE IF EXISTS ConfigItem;
CREATE TABLE ConfigItem
(
	ConfigTicket TEXT,
	ConfigName TEXT,
	ConfigValue TEXT
);
INSERT INTO ConfigItem(ConfigName, ConfigValue) VALUES('CreateDate', datetime('now'));
