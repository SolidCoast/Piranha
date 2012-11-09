--
-- Database version 3 Update script
--
-- 2012-03-18

--
-- Property
--
-- Properties can now be attached to several entities so the foreign key constraint 
-- to page table is dropped and the relation columns is renamed. Also there is no
-- need to know if the entity the property attached to is in draft or not as the
-- draft itself has a draft property.
--
EXEC sp_rename 'property', 'property_old';
EXEC sp_rename 'pk_property_id', 'pk_property_id_old', 'object';
EXEC sp_rename 'fk_property_created_by', 'fk_property_created_by_old', 'object';
EXEC sp_rename 'fk_property_updated_by', 'fk_property_updated_by_old', 'object';
CREATE TABLE property (
	property_id UNIQUEIDENTIFIER NOT NULL,
	property_draft BIT NOT NULL default(1),
	property_parent_id UNIQUEIDENTIFIER NOT NULL,
	property_name NVARCHAR(64) NOT NULL,
	property_value NTEXT NULL,
	property_created DATETIME NOT NULL,
	property_updated DATETIME NOT NULL,
	property_created_by UNIQUEIDENTIFIER NOT NULL,
	property_updated_by UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_property_id PRIMARY KEY (property_id, property_draft),
	CONSTRAINT fk_property_created_by FOREIGN KEY (property_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_property_updated_by FOREIGN KEY (property_updated_by) REFERENCES sysuser (sysuser_id)
);
INSERT INTO property (
	property_id, property_draft, property_parent_id, property_name, property_value, 
	property_created, property_updated, property_created_by, property_updated_by)
SELECT property_id, property_draft, property_page_id AS property_parent_id, property_name, property_value, property_created, 
	property_updated, property_created_by, property_updated_by FROM property_old;
DROP TABLE property_old;