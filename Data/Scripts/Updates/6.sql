--
-- Database version 6 Update script
--
-- 2012-05-29
--
-- Add drafting to relations.

-- Update relations to be drafted.
ALTER TABLE relation DROP CONSTRAINT pk_relation_id;
ALTER TABLE relation ADD relation_draft BIT NOT NULL default(1);
ALTER TABLE relation ADD CONSTRAINT pk_relation_id PRIMARY KEY (relation_id, relation_draft);

-- Insert published relations.
INSERT INTO relation (relation_id, relation_draft, relation_type, relation_data_id, relation_related_id)
	SELECT
		relation_id,
		0,
		relation_type,
		relation_data_id,
		relation_related_id
	FROM
		relation
	WHERE
		relation_draft = 1;