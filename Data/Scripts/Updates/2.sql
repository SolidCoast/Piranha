--
-- Database version 2 Update script
--
-- 2012-03-17

-- User
ALTER TABLE sysuser ADD sysuser_culture NVARCHAR(5) NULL;

-- Page template
ALTER TABLE pagetemplate ALTER COLUMN pagetemplate_page_regions NTEXT NULL;
ALTER TABLE pagetemplate ALTER COLUMN pagetemplate_properties NTEXT NULL;

-- Post template
ALTER TABLE posttemplate DROP COLUMN posttemplate_view;
ALTER TABLE posttemplate DROP COLUMN posttemplate_manager_view;
ALTER TABLE posttemplate DROP COLUMN posttemplate_manager_controller;
ALTER TABLE	posttemplate ADD posttemplate_properties NTEXT NULL;
ALTER TABLE posttemplate ADD posttemplate_controller_show BIT NOT NULL default(0);
ALTER TABLE posttemplate ADD posttemplate_archive_controller NVARCHAR(128) NULL;
ALTER TABLE posttemplate ADD posttemplate_archive_controller_show BIT NOT NULL default(0);

-- Attachment
DROP TABLE attachment;

-- Page
ALTER TABLE page ADD page_attachments NTEXT NULL;

-- Post
ALTER TABLE post ADD post_attachments NTEXT NULL;
ALTER TABLE post ADD post_controller NVARCHAR(128) NULL;
ALTER TABLE post ADD post_archive_controller NVARCHAR(128) NULL;

-- Default params
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('08E8A582-7825-43B2-A12D-2522889F04BE', 'IMAGE_MAX_WIDTH', 940, 'Max-storlek för bilder som laddas upp.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

