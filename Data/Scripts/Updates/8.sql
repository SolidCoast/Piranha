--
-- Database version 8 Update script
--
-- 2012-06-28
--
-- This update adds the namespace table and adds relationships between the 
-- permalink namespace.

-- Create namespace table
CREATE TABLE [namespace] (
	[namespace_id] UNIQUEIDENTIFIER NOT NULL,
	[namespace_internal_id] NVARCHAR(32) NOT NULL,
	[namespace_name] NVARCHAR(64) NOT NULL,
	[namespace_description] NVARCHAR(255) NULL,
	[namespace_created] DATETIME NOT NULL,
	[namespace_updated] DATETIME NOT NULL,
	[namespace_created_by] UNIQUEIDENTIFIER NOT NULL,
	[namespace_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_namespace_id PRIMARY KEY ([namespace_id]),
	CONSTRAINT fk_namespace_created_by FOREIGN KEY ([namespace_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_namespace_updated_by FOREIGN KEY ([namespace_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

-- Alter permalink table
ALTER TABLE [permalink] ADD permalink_namespace_id UNIQUEIDENTIFIER NULL;
DROP INDEX index_permalink_name ON permalink;

-- Insert default namespaces
INSERT INTO [namespace] ([namespace_id], [namespace_internal_id], [namespace_name], [namespace_description], [namespace_created], [namespace_updated],
	[namespace_created_by], [namespace_updated_by])
	VALUES ('8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03', 'DEFAULT', 'Default namespace', 'This is the default namespace for all pages and posts.',
		GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO [namespace] ([namespace_id], [namespace_internal_id], [namespace_name], [namespace_description], [namespace_created], [namespace_updated],
	[namespace_created_by], [namespace_updated_by])
	VALUES ('AE46C4C4-20F7-4582-888D-DFC148FE9067', 'CATEGORY', 'Category namespace', 'This is the namespace for all categories.',
		GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Now add all namespace references to existing permalinks
UPDATE permalink SET permalink_namespace_id = '8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03' WHERE permalink_type IN ('PAGE', 'POST');
UPDATE permalink SET permalink_namespace_id = 'AE46C4C4-20F7-4582-888D-DFC148FE9067' WHERE permalink_type = 'CATEGORY';

-- Set namespace_id to NOT NULL and create the index.
ALTER TABLE [permalink] ALTER COLUMN permalink_namespace_id UNIQUEIDENTIFIER NOT NULL;
ALTER TABLE [permalink] ADD CONSTRAINT fk_permalink_namespace_id FOREIGN KEY ([permalink_namespace_id]) REFERENCES [namespace] ([namespace_id]);
CREATE UNIQUE INDEX index_permalink_name ON permalink (permalink_namespace_id, permalink_name);
