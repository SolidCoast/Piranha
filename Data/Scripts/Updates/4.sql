--
-- Database version 4 Update script
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
INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('095502DD-D655-4001-86F9-97D18222A548', 'SITEMAP_EXPANDED_LEVELS', '0', 'The number of pre-expanded levels in the manager panel for the page list.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
