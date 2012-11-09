--
-- Database version 13 Update script
--
-- 2012-08-27
--
-- This update adds support for complex regions & properties

-- Region templates
CREATE TABLE [regiontemplate] (
	[regiontemplate_id] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_template_id] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_internal_id] NVARCHAR(32) NOT NULL,
	[regiontemplate_seqno] INT NOT NULL default(1),
	[regiontemplate_name] NVARCHAR(64) NOT NULL,
	[regiontemplate_description] NVARCHAR(255) NULL,
	[regiontemplate_type] NVARCHAR(255) NOT NULL,
	[regiontemplate_created] DATETIME NOT NULL,
	[regiontemplate_updated] DATETIME NOT NULL,
	[regiontemplate_created_by] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_regiontemplate_id PRIMARY KEY ([regiontemplate_id]),
	CONSTRAINT fk_regiontemplate_created_by FOREIGN KEY ([regiontemplate_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_regiontemplate_updated_by FOREIGN KEY ([regiontemplate_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);
CREATE UNIQUE INDEX [index_regiontemplate_internal_id] ON [regiontemplate] ([regiontemplate_template_id], [regiontemplate_internal_id]);

-- Add a link to region templates from the regions.
ALTER TABLE [region] ADD [region_regiontemplate_id] UNIQUEIDENTIFIER NULL;
ALTER TABLE [region] ADD CONSTRAINT fk_region_regiontemplate FOREIGN KEY ([region_regiontemplate_id]) REFERENCES [regiontemplate] ([regiontemplate_id]) ON DELETE CASCADE;

-- Allow nulls for region name.
ALTER TABLE [region] ALTER COLUMN [region_name] NVARCHAR(64) NULL;

-- Add possibility to disable pages for some groups.
ALTER TABLE [page] ADD [page_group_disable_id] NTEXT NULL;
