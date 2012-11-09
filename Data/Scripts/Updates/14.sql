--
-- Database version 14 Update script
--
-- 2012-09-14
--
-- This update adds the extension table.

CREATE TABLE [extension] (
	[extension_id] UNIQUEIDENTIFIER NOT NULL,
	[extension_draft] BIT NOT NULL default(1),
	[extension_parent_id] UNIQUEIDENTIFIER NOT NULL,
	[extension_body] NTEXT NULL,
	[extension_type] NVARCHAR(255) NOT NULL,
	[extension_created] DATETIME NOT NULL,
	[extension_updated] DATETIME NOT NULL,
	[extension_created_by] UNIQUEIDENTIFIER NOT NULL,
	[extension_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_extension_id PRIMARY KEY ([extension_id], [extension_draft]),
	CONSTRAINT fk_extension_created_by FOREIGN KEY ([extension_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_extension_updated_by FOREIGN KEY ([extension_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);
