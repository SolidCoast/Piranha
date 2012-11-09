CREATE TABLE sysuser (
	sysuser_id CHAR(38) NOT NULL,
	sysuser_login VARCHAR(64) NOT NULL,
	sysuser_password VARCHAR(64) NULL,
	sysuser_firstname VARCHAR(128) NULL,
	sysuser_surname VARCHAR(128) NULL,
	sysuser_email VARCHAR(128) NULL,
	sysuser_group_id CHAR(38) NULL,
	sysuser_culture VARCHAR(5) NULL,
	sysuser_created DATETIME NOT NULL,
	sysuser_updated DATETIME NOT NULL,
	sysuser_created_by CHAR(38) NULL,
	sysuser_updated_by CHAR(38) NULL,
	CONSTRAINT pk_sysuser_id PRIMARY KEY (sysuser_id)
);

CREATE TABLE sysgroup (
	sysgroup_id CHAR(38) NOT NULL,
	sysgroup_parent_id CHAR(38) NULL,
	sysgroup_name VARCHAR(64) NOT NULL,
	sysgroup_description VARCHAR(255) NULL,
	sysgroup_created DATETIME NOT NULL,
	sysgroup_updated DATETIME NOT NULL,
	sysgroup_created_by CHAR(38) NULL,
	sysgroup_updated_by CHAR(38) NULL,
	CONSTRAINT pk_sysgroup_id PRIMARY KEY (sysgroup_id),
	CONSTRAINT fk_sysgroup_created_by FOREIGN KEY (sysgroup_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_sysgroup_updated_by FOREIGN KEY (sysgroup_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE sysaccess (
	sysaccess_id CHAR(38) NOT NULL,
	sysaccess_group_id CHAR(38) NOT NULL,
	sysaccess_function VARCHAR(64) NOT NULL,
	sysaccess_description VARCHAR(255) NULL,
	sysaccess_locked BIT NOT NULL default false,
	sysaccess_created DATETIME NOT NULL,
	sysaccess_updated DATETIME NOT NULL,
	sysaccess_created_by CHAR(38) NULL,
	sysaccess_updated_by CHAR(38) NULL,
	CONSTRAINT pk_sysaccess_id PRIMARY KEY (sysaccess_id),
	CONSTRAINT fk_sysaccess_created_by FOREIGN KEY (sysaccess_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_sysaccess_updated_by FOREIGN KEY (sysaccess_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE sysparam (
	sysparam_id CHAR(38) NOT NULL,
	sysparam_name VARCHAR(64) NOT NULL,
	sysparam_value VARCHAR(128) NULL,
	sysparam_description VARCHAR(255) NULL,
	sysparam_locked BIT NOT NULL default false,
	sysparam_created DATETIME NOT NULL,
	sysparam_updated DATETIME NOT NULL,
	sysparam_created_by CHAR(38) NULL,
	sysparam_updated_by CHAR(38) NULL,
	CONSTRAINT pk_sysparam_id PRIMARY KEY (sysparam_id),
	CONSTRAINT fk_sysparam_created_by FOREIGN KEY (sysparam_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_sysparam_updated_by FOREIGN KEY (sysparam_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE syslog (
	syslog_id CHAR(38) NOT NULL,
	syslog_parent_id CHAR(38) NOT NULL,
	syslog_parent_type VARCHAR(128) NOT NULL,
	syslog_action VARCHAR(64) NOT NULL,
	syslog_created DATETIME NOT NULL,
	syslog_updated DATETIME NOT NULL,
	syslog_created_by CHAR(38) NOT NULL,
	syslog_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_syslog_id PRIMARY KEY (syslog_id),
	CONSTRAINT fk_syslog_created_by FOREIGN KEY (syslog_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_syslog_updated_by FOREIGN KEY (syslog_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE pagetemplate (
	pagetemplate_id CHAR(38) NOT NULL,
	pagetemplate_name VARCHAR(64) NOT NULL,
	pagetemplate_description VARCHAR(255) NULL,
	pagetemplate_preview TEXT NULL,
	pagetemplate_page_regions TEXT NULL,
	pagetemplate_properties TEXT NULL,
	pagetemplate_controller VARCHAR(128) NULL,
	pagetemplate_controller_show BIT NOT NULL default false,
	pagetemplate_redirect VARCHAR(128) NULL,
	pagetemplate_redirect_show BIT NOT NULL default false,
	pagetemplate_created DATETIME NOT NULL,
	pagetemplate_updated DATETIME NOT NULL,
	pagetemplate_created_by CHAR(38) NOT NULL,
	pagetemplate_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_pagetemplate_id PRIMARY KEY (pagetemplate_id),
	CONSTRAINT fk_pagetemplate_created_by FOREIGN KEY (pagetemplate_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_pagetemplate_updated_by FOREIGN KEY (pagetemplate_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE posttemplate (
	posttemplate_id CHAR(38) NOT NULL,
	posttemplate_name VARCHAR(64) NOT NULL,
	posttemplate_description VARCHAR(255) NULL,
	posttemplate_preview TEXT NULL,
	posttemplate_properties TEXT NULL,
	posttemplate_controller VARCHAR(128) NULL,
	posttemplate_controller_show BIT NOT NULL default false,
	posttemplate_archive_controller VARCHAR(128) NULL,	
	posttemplate_archive_controller_show BIT NOT NULL default false,
	posttemplate_rss BIT NOT NULL default true,
	posttemplate_created DATETIME NOT NULL,
	posttemplate_updated DATETIME NOT NULL,
	posttemplate_created_by CHAR(38) NOT NULL,
	posttemplate_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_posttemplate_id PRIMARY KEY (posttemplate_id),
	CONSTRAINT fk_posttemplate_created_by FOREIGN KEY (posttemplate_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_posttemplate_updated_by FOREIGN KEY (posttemplate_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE namespace (
	namespace_id CHAR(38) NOT NULL,
	namespace_internal_id VARCHAR(32) NOT NULL,
	namespace_name VARCHAR(64) NOT NULL,
	namespace_description VARCHAR(255) NULL,
	namespace_created DATETIME NOT NULL,
	namespace_updated DATETIME NOT NULL,
	namespace_created_by CHAR(38) NOT NULL,
	namespace_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_namespace_id PRIMARY KEY (namespace_id),
	CONSTRAINT fk_namespace_created_by FOREIGN KEY (namespace_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_namespace_updated_by FOREIGN KEY (namespace_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE permalink (
	permalink_id CHAR(38) NOT NULL,
	permalink_namespace_id CHAR(38) NOT NULL,
	permalink_type VARCHAR(16) NOT NULL,
	permalink_name VARCHAR(128) NOT NULL,
	permalink_created DATETIME NOT NULL,
	permalink_updated DATETIME NOT NULL,
	permalink_created_by CHAR(38) NOT NULL,
	permalink_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_permalink_id PRIMARY KEY (permalink_id),
	CONSTRAINT fk_permalink_namespace_id FOREIGN KEY (permalink_namespace_id) REFERENCES namespace (namespace_id)
);
CREATE UNIQUE INDEX index_permalink_name ON permalink (permalink_namespace_id, permalink_name);

CREATE TABLE category (
	category_id CHAR(38) NOT NULL,
	category_parent_id CHAR(38) NULL,
	category_permalink_id CHAR(38) NOT NULL,
	category_name VARCHAR(64) NOT NULL,
	category_description VARCHAR(255) NULL,
	category_created DATETIME NOT NULL,
	category_updated DATETIME NOT NULL,
	category_created_by CHAR(38) NOT NULL,
	category_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_category_id PRIMARY KEY (category_id),
	CONSTRAINT fk_category_permalink_id FOREIGN KEY (category_permalink_id) REFERENCES permalink (permalink_id),
	CONSTRAINT fk_category_created_by FOREIGN KEY (category_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_category_updated_by FOREIGN KEY (category_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE relation (
	relation_id CHAR(38) NOT NULL,
	relation_draft BIT NOT NULL default true,
	relation_type VARCHAR(16) NOT NULL,
	relation_data_id CHAR(38) NOT NULL,
	relation_related_id CHAR(38) NOT NULL,
	CONSTRAINT pk_relation_id PRIMARY KEY (relation_id, relation_draft)
);

CREATE TABLE page (
	page_id CHAR(38) NOT NULL,
	page_draft BIT NOT NULL default true,
	page_template_id CHAR(38) NOT NULL,
	page_group_id CHAR(38) NULL,
	page_group_disable_id TEXT NULL,
	page_parent_id CHAR(38) NULL,
	page_permalink_id CHAR(38) NOT NULL,
	page_seqno INT NOT NULL DEFAULT '1',
	page_title VARCHAR(128) NOT NULL,
	page_navigation_title VARCHAR(128) NULL,
	page_is_hidden BIT NOT NULL DEFAULT false,
	page_keywords VARCHAR(128) NULL,
	page_description VARCHAR(255) NULL,
	page_attachments TEXT NULL,
	page_controller VARCHAR(128) NULL,
	page_redirect VARCHAR(128) NULL,
	page_created DATETIME NOT NULL,
	page_updated DATETIME NOT NULL,
	page_published DATETIME NULL,
	page_last_published DATETIME NULL,
	page_created_by CHAR(38) NOT NULL,
	page_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_page_id PRIMARY KEY (page_id, page_draft),
	CONSTRAINT fk_page_template_id FOREIGN KEY (page_template_id) REFERENCES pagetemplate (pagetemplate_id),
	CONSTRAINT fk_page_permalink_id FOREIGN KEY (page_permalink_id) REFERENCES permalink (permalink_id),
	CONSTRAINT fk_page_created_by FOREIGN KEY (page_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_page_updated_by FOREIGN KEY (page_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE regiontemplate (
	regiontemplate_id CHAR(38) NOT NULL,
	regiontemplate_template_id CHAR(38) NOT NULL,
	regiontemplate_internal_id VARCHAR(32) NOT NULL,
	regiontemplate_seqno INT NOT NULL default '1',
	regiontemplate_name VARCHAR(64) NOT NULL,
	regiontemplate_description VARCHAR(255) NULL,
	regiontemplate_type VARCHAR(255) NOT NULL,
	regiontemplate_created DATETIME NOT NULL,
	regiontemplate_updated DATETIME NOT NULL,
	regiontemplate_created_by CHAR(38) NOT NULL,
	regiontemplate_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_regiontemplate_id PRIMARY KEY (regiontemplate_id),
	CONSTRAINT fk_regiontemplate_created_by FOREIGN KEY (regiontemplate_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_regiontemplate_updated_by FOREIGN KEY (regiontemplate_updated_by) REFERENCES sysuser (sysuser_id)
);
CREATE UNIQUE INDEX index_regiontemplate_internal_id ON regiontemplate (regiontemplate_template_id, regiontemplate_internal_id);

CREATE TABLE region (
	region_id CHAR(38) NOT NULL,
	region_draft BIT NOT NULL default true,
	region_page_id CHAR(38) NULL,
	region_page_draft BIT NOT NULL default true,
	region_regiontemplate_id CHAR(38) NULL,
	region_name VARCHAR(64) NULL,
	region_body TEXT NULL,
	region_created DATETIME NOT NULL,
	region_updated DATETIME NOT NULL,
	region_created_by CHAR(38) NOT NULL,
	region_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_region_id PRIMARY KEY (region_id, region_draft),
	CONSTRAINT fk_region_page_id FOREIGN KEY (region_page_id, region_page_draft) REFERENCES page (page_id, page_draft),
	CONSTRAINT fk_region_regiontemplate FOREIGN KEY (region_regiontemplate_id) REFERENCES regiontemplate (regiontemplate_id) ON DELETE CASCADE,
	CONSTRAINT fk_region_created_by FOREIGN KEY (region_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_region_updated_by FOREIGN KEY (region_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE property (
	property_id CHAR(38) NOT NULL,
	property_draft BIT NOT NULL default true,
	property_parent_id CHAR(38) NOT NULL,
	property_name VARCHAR(64) NOT NULL,
	property_value TEXT NULL,
	property_created DATETIME NOT NULL,
	property_updated DATETIME NOT NULL,
	property_created_by CHAR(38) NOT NULL,
	property_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_property_id PRIMARY KEY (property_id, property_draft),
	CONSTRAINT fk_property_created_by FOREIGN KEY (property_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_property_updated_by FOREIGN KEY (property_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE post (
	post_id CHAR(38) NOT NULL,
	post_draft BIT NOT NULL default true,
	post_template_id CHAR(38) NOT NULL,
	post_permalink_id CHAR(38) NOT NULL,
	post_title VARCHAR(128) NOT NULL,
	post_keywords VARCHAR(128) NULL,
	post_description VARCHAR(255) NULL,
	post_rss BIT NOT NULL default true,
	post_excerpt VARCHAR(255) NULL,
	post_body TEXT NULL,
	post_attachments TEXT NULL,
	post_controller VARCHAR(128) NULL,
	post_archive_controller VARCHAR(128) NULL,
	post_created DATETIME NOT NULL,
	post_updated DATETIME NOT NULL,
	post_published DATETIME NULL,
	post_last_published DATETIME NULL,
	post_created_by CHAR(38) NOT NULL,
	post_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_post_id PRIMARY KEY (post_id, post_draft),
	CONSTRAINT fk_post_template_id FOREIGN KEY (post_template_id) REFERENCES posttemplate (posttemplate_id),
	CONSTRAINT fk_post_permalink_id FOREIGN KEY (post_permalink_id) REFERENCES permalink (permalink_id),
	CONSTRAINT fk_post_created_by FOREIGN KEY (post_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_post_updated_by FOREIGN KEY (post_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE content (
	content_id CHAR(38) NOT NULL,
	content_parent_id CHAR(38) NULL,
	content_filename VARCHAR(128) NULL,
	content_name VARCHAR(128) NULL,
	content_type VARCHAR(255) NULL,
	content_size INT NOT NULL default '0',
	content_image BIT NOT NULL default false,
	content_folder BIT NOT NULL default false,
	content_width INT NULL,
	content_height INT NULL,
	content_alt VARCHAR(128) NULL,
	content_description VARCHAR(255) NULL,
	content_created DATETIME NOT NULL,
	content_updated DATETIME NOT NULL,
	content_created_by CHAR(38) NOT NULL,
	content_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_content_id PRIMARY KEY (content_id),
	CONSTRAINT fk_content_created_by FOREIGN KEY (content_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_content_updated_by FOREIGN KEY (content_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE upload (
	upload_id CHAR(38) NOT NULL,
	upload_parent_id CHAR(38) NULL,
	upload_filename VARCHAR(128) NOT NULL,
	upload_type VARCHAR(255) NOT NULL,
	upload_created DATETIME NOT NULL,
	upload_updated DATETIME NOT NULL,
	upload_created_by CHAR(38) NOT NULL,
	upload_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_upload_id PRIMARY KEY (upload_id),
	CONSTRAINT fk_upload_created_by FOREIGN KEY (upload_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_upload_updated_by FOREIGN KEY (upload_updated_by) REFERENCES sysuser (sysuser_id)
);

CREATE TABLE extension (
	extension_id CHAR(38) NOT NULL,
	extension_draft BIT NOT NULL default true,
	extension_parent_id CHAR(38) NOT NULL,
	extension_body TEXT NULL,
	extension_type VARCHAR(255) NOT NULL,
	extension_created DATETIME NOT NULL,
	extension_updated DATETIME NOT NULL,
	extension_created_by CHAR(38) NOT NULL,
	extension_updated_by CHAR(38) NOT NULL,
	CONSTRAINT pk_extension_id PRIMARY KEY (extension_id, extension_draft),
	CONSTRAINT fk_extension_created_by FOREIGN KEY (extension_created_by) REFERENCES sysuser (sysuser_id),
	CONSTRAINT fk_extension_updated_by FOREIGN KEY (extension_updated_by) REFERENCES sysuser (sysuser_id)
);
