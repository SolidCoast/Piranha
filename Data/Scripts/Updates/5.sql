--
-- Database version 5 Update script
--
-- 2012-05-03

-- New parameters
INSERT INTO sysaccess (sysaccess_id, sysaccess_group_id, sysaccess_function, sysaccess_description, sysaccess_locked,
	sysaccess_created, sysaccess_updated, sysaccess_created_by, sysaccess_updated_by)
VALUES ('3B074724-69DF-4165-81D8-2157B25FE96F', '7c536b66-d292-4369-8f37-948b32229b83', 
	'ADMIN_EXTERNAL', 'Access to configure connections to external systems.', 1, GETDATE(), GETDATE(), 
	'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');

-- Post permalinks
ALTER TABLE post ADD post_permalink_id UNIQUEIDENTIFIER NULL;
ALTER TABLE post ADD CONSTRAINT fk_post_permalink_id FOREIGN KEY (post_permalink_id) REFERENCES permalink (permalink_id);
UPDATE post SET post_permalink_id = 
	(SELECT permalink_id FROM permalink WHERE permalink_parent_id = post_id) ;
ALTER TABLE post ALTER COLUMN post_permalink_id UNIQUEIDENTIFIER NOT NULL;

-- Page permalinks
ALTER TABLE page ADD page_permalink_id UNIQUEIDENTIFIER NULL;
ALTER TABLE page ADD CONSTRAINT fk_page_permalink_id FOREIGN KEY (page_permalink_id) REFERENCES permalink (permalink_id);
UPDATE page SET page_permalink_id = 
	(SELECT permalink_id FROM permalink WHERE permalink_parent_id = page_id) ;
ALTER TABLE page ALTER COLUMN page_permalink_id UNIQUEIDENTIFIER NOT NULL;

-- Category permalinks
ALTER TABLE category ADD category_permalink_id UNIQUEIDENTIFIER NULL;
ALTER TABLE category ADD CONSTRAINT fk_category_permalink_id FOREIGN KEY (category_permalink_id) REFERENCES permalink (permalink_id);
UPDATE category SET category_permalink_id = 
	(SELECT permalink_id FROM permalink WHERE permalink_parent_id = category_id) ;
ALTER TABLE category ALTER COLUMN category_permalink_id UNIQUEIDENTIFIER NOT NULL;

-- Delete parent id from permalink
ALTER TABLE permalink DROP COLUMN permalink_parent_id;

