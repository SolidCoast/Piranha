--
-- Database version 12 Update script
--
-- 2012-08-15
--
-- This update adds the syslog entitiy.

-- Syslog
CREATE TABLE [syslog] (
	[syslog_id] UNIQUEIDENTIFIER NOT NULL,
	[syslog_parent_id] UNIQUEIDENTIFIER NOT NULL,
	[syslog_parent_type] NVARCHAR(128) NOT NULL,
	[syslog_action] NVARCHAR(64) NOT NULL,
	[syslog_created] DATETIME NOT NULL,
	[syslog_updated] DATETIME NOT NULL,
	[syslog_created_by] UNIQUEIDENTIFIER NOT NULL,
	[syslog_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_syslog_id PRIMARY KEY ([syslog_id]),
	CONSTRAINT fk_syslog_created_by FOREIGN KEY ([syslog_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_syslog_updated_by FOREIGN KEY ([syslog_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);
