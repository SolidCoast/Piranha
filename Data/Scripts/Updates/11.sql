--
-- Database version 11 Update script
--
-- 2012-08-08
--
-- This update adds columns and default data for rss-feeds.

ALTER TABLE [posttemplate] ADD [posttemplate_rss] BIT NOT NULL default(1);
ALTER TABLE [post] ADD [post_rss] BIT NOT NULL default(1);

INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('40230360-71CE-441E-A8DF-D50CFA79ACC2', 'RSS_NUM_POSTS', 10, 'The maximum number posts to be exported in a feed. For an infinite amount of posts, use the value 0.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('A64A8479-8125-47BA-9980-B30B36E744D3', 'RSS_USE_EXCERPT', '1', 'Weather to use an excerpt in the feeds or export the full content.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
