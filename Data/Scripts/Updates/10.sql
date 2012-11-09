--
-- Database version 10 Update script
--
-- 2012-08-06
--
-- This update adds folders & names to images & documents.

ALTER TABLE [content] ADD [content_parent_id] UNIQUEIDENTIFIER NULL;
ALTER TABLE [content] ADD [content_folder] BIT NOT NULL default(0);
ALTER TABLE [content] ADD [content_name] NVARCHAR(128) NULL;
ALTER TABLE [content] ALTER COLUMN [content_filename] NVARCHAR(128) NULL;
ALTER TABLE [content] ALTER COLUMN [content_type] NVARCHAR(255) NULL;
