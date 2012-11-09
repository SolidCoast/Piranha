--
-- Database version 15 Update script
--
-- 2012-10-10
--
-- Adds sysparam for hierarchical permalinks.

INSERT INTO sysparam (sysparam_id, sysparam_name, sysparam_value, sysparam_description, sysparam_locked,
	sysparam_created, sysparam_updated, sysparam_created_by, sysparam_updated_by)
VALUES ('5A0D6307-F041-41A1-B63A-563E712F3B8C', 'HIERARCHICAL_PERMALINKS', '0', 'Weather or not permalink generation should be hierarchical.', 1,
	GETDATE(), GETDATE(), 'ca19d4e7-92f0-42f6-926a-68413bbdafbc', 'ca19d4e7-92f0-42f6-926a-68413bbdafbc');
