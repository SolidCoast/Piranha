--
-- Database version 7 Update script
--
-- 2012-06-18
--
-- Uppdate content type

-- Update relations to be drafted.
ALTER TABLE content ALTER COLUMN content_type NVARCHAR(255) NOT NULL;