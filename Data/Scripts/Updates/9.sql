--
-- Database version 9 Update script
--
-- 2012-06-29
--
-- This update adds the keywords and descripton on the post entity for
-- meta tag generation.

ALTER TABLE [post] ADD [post_keywords] NVARCHAR(128) NULL;
ALTER TABLE [post] ADD [post_description] NVARCHAR(255) NULL;
