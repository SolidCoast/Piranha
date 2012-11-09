--
-- Database version 16 Update script
--
-- 2012-10-23
--
-- Updates the upload entity to match the standard entity.

ALTER TABLE [upload] ALTER COLUMN [upload_type] NVARCHAR(255) NOT NULL;
ALTER TABLE [upload] ADD [upload_updated] DATETIME NULL;
ALTER TABLE [upload] ADD [upload_updated_by] UNIQUEIDENTIFIER NULL;
ALTER TABLE [upload] ADD CONSTRAINT [fk_upload_uploaded_by] FOREIGN KEY ([upload_updated_by]) REFERENCES [sysuser] ([sysuser_id]);

UPDATE [upload] SET [upload_updated] = [upload_created], [upload_updated_by] = [upload_created_by] ;

ALTER TABLE [upload] ALTER COLUMN [upload_updated] DATETIME NOT NULL;
ALTER TABLE [upload] ALTER COLUMN [upload_updated_by] UNIQUEIDENTIFIER NOT NULL;
