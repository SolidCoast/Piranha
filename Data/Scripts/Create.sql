CREATE TABLE [sysuser] (
	[sysuser_id] UNIQUEIDENTIFIER NOT NULL,
	[sysuser_login] NVARCHAR(64) NOT NULL,
	[sysuser_password] NVARCHAR(64) NULL,
	[sysuser_firstname] NVARCHAR(128) NULL,
	[sysuser_surname] NVARCHAR(128) NULL,
	[sysuser_email] NVARCHAR(128) NULL,
	[sysuser_group_id] UNIQUEIDENTIFIER NULL,
	[sysuser_culture] NVARCHAR(5) NULL,
	[sysuser_created] DATETIME NOT NULL,
	[sysuser_updated] DATETIME NOT NULL,
	[sysuser_created_by] UNIQUEIDENTIFIER NULL,
	[sysuser_updated_by] UNIQUEIDENTIFIER NULL,
	CONSTRAINT pk_sysuser_id PRIMARY KEY ([sysuser_id])
);

CREATE TABLE [sysgroup] (
	[sysgroup_id] UNIQUEIDENTIFIER NOT NULL,
	[sysgroup_parent_id] UNIQUEIDENTIFIER NULL,
	[sysgroup_name] NVARCHAR(64) NOT NULL,
	[sysgroup_description] NVARCHAR(255) NULL,
	[sysgroup_created] DATETIME NOT NULL,
	[sysgroup_updated] DATETIME NOT NULL,
	[sysgroup_created_by] UNIQUEIDENTIFIER NULL,
	[sysgroup_updated_by] UNIQUEIDENTIFIER NULL,
	CONSTRAINT pk_sysgroup_id PRIMARY KEY ([sysgroup_id]),
	CONSTRAINT fk_sysgroup_created_by FOREIGN KEY ([sysgroup_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_sysgroup_updated_by FOREIGN KEY ([sysgroup_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [sysaccess] (
	[sysaccess_id] UNIQUEIDENTIFIER NOT NULL,
	[sysaccess_group_id] UNIQUEIDENTIFIER NOT NULL,
	[sysaccess_function] NVARCHAR(64) NOT NULL,
	[sysaccess_description] NVARCHAR(255) NULL,
	[sysaccess_locked] BIT NOT NULL default(0),
	[sysaccess_created] DATETIME NOT NULL,
	[sysaccess_updated] DATETIME NOT NULL,
	[sysaccess_created_by] UNIQUEIDENTIFIER NULL,
	[sysaccess_updated_by] UNIQUEIDENTIFIER NULL,
	CONSTRAINT pk_sysaccess_id PRIMARY KEY ([sysaccess_id]),
	CONSTRAINT fk_sysaccess_created_by FOREIGN KEY ([sysaccess_created_by]) REFERENCES [sysuser] (sysuser_id),
	CONSTRAINT fk_sysaccess_updated_by FOREIGN KEY ([sysaccess_updated_by]) REFERENCES [sysuser] (sysuser_id)
);

CREATE TABLE [sysparam] (
	[sysparam_id] UNIQUEIDENTIFIER NOT NULL,
	[sysparam_name] NVARCHAR(64) NOT NULL,
	[sysparam_value] NVARCHAR(128) NULL,
	[sysparam_description] NVARCHAR(255) NULL,
	[sysparam_locked] BIT NOT NULL default(0),
	[sysparam_created] DATETIME NOT NULL,
	[sysparam_updated] DATETIME NOT NULL,
	[sysparam_created_by] UNIQUEIDENTIFIER NULL,
	[sysparam_updated_by] UNIQUEIDENTIFIER NULL,
	CONSTRAINT pk_sysparam_id PRIMARY KEY ([sysparam_id]),
	CONSTRAINT fk_sysparam_created_by FOREIGN KEY ([sysparam_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_sysparam_updated_by FOREIGN KEY ([sysparam_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [syslog] (
	[syslog_id] UNIQUEIDENTIFIER NOT NULL,
	[syslog_parent_id] UNIQUEIDENTIFIER NOT NULL,
	[syslog_parent_type] NVARCHAR(128) NOT NULL,
	[syslog_action] NVARCHAR(64) NOT NULL,
	[syslog_created] DATETIME NOT NULL,
	[syslog_updated] DATETIME NOT NULL,
	[syslog_created_by] UNIQUEIDENTIFIER NOT NULL,
	[syslog_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_syslog_id PRIMARY KEY ([syslog_id]),
	CONSTRAINT fk_syslog_created_by FOREIGN KEY ([syslog_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_syslog_updated_by FOREIGN KEY ([syslog_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [pagetemplate] (
	[pagetemplate_id] UNIQUEIDENTIFIER NOT NULL,
	[pagetemplate_name] NVARCHAR(64) NOT NULL,
	[pagetemplate_description] NVARCHAR(255) NULL,
	[pagetemplate_preview] NTEXT NULL,
	[pagetemplate_page_regions] NTEXT NULL,
	[pagetemplate_properties] NTEXT NULL,
	[pagetemplate_controller] NVARCHAR(128) NULL,
	[pagetemplate_controller_show] BIT NOT NULL default(0),
	[pagetemplate_redirect] NVARCHAR(128) NULL,
	[pagetemplate_redirect_show] BIT NOT NULL default(0),
	[pagetemplate_created] DATETIME NOT NULL,
	[pagetemplate_updated] DATETIME NOT NULL,
	[pagetemplate_created_by] UNIQUEIDENTIFIER NOT NULL,
	[pagetemplate_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_pagetemplate_id PRIMARY KEY ([pagetemplate_id]),
	CONSTRAINT fk_pagetemplate_created_by FOREIGN KEY ([pagetemplate_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_pagetemplate_updated_by FOREIGN KEY ([pagetemplate_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [posttemplate] (
	[posttemplate_id] UNIQUEIDENTIFIER NOT NULL,
	[posttemplate_name] NVARCHAR(64) NOT NULL,
	[posttemplate_description] NVARCHAR(255) NULL,
	[posttemplate_preview] NTEXT NULL,
	[posttemplate_properties] NTEXT NULL,
	[posttemplate_controller] NVARCHAR(128) NULL,
	[posttemplate_controller_show] BIT NOT NULL default(0),
	[posttemplate_archive_controller] NVARCHAR(128) NULL,	
	[posttemplate_archive_controller_show] BIT NOT NULL default(0),
	[posttemplate_rss] BIT NOT NULL DEFAULT(1),
	[posttemplate_created] DATETIME NOT NULL,
	[posttemplate_updated] DATETIME NOT NULL,
	[posttemplate_created_by] UNIQUEIDENTIFIER NOT NULL,
	[posttemplate_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_posttemplate_id PRIMARY KEY ([posttemplate_id]),
	CONSTRAINT fk_posttemplate_created_by FOREIGN KEY ([posttemplate_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_posttemplate_updated_by FOREIGN KEY ([posttemplate_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [namespace] (
	[namespace_id] UNIQUEIDENTIFIER NOT NULL,
	[namespace_internal_id] NVARCHAR(32) NOT NULL,
	[namespace_name] NVARCHAR(64) NOT NULL,
	[namespace_description] NVARCHAR(255) NULL,
	[namespace_created] DATETIME NOT NULL,
	[namespace_updated] DATETIME NOT NULL,
	[namespace_created_by] UNIQUEIDENTIFIER NOT NULL,
	[namespace_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_namespace_id PRIMARY KEY ([namespace_id]),
	CONSTRAINT fk_namespace_created_by FOREIGN KEY ([namespace_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_namespace_updated_by FOREIGN KEY ([namespace_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [permalink] (
	[permalink_id] UNIQUEIDENTIFIER NOT NULL,
	[permalink_namespace_id] UNIQUEIDENTIFIER NOT NULL,
	[permalink_type] NVARCHAR(16) NOT NULL,
	[permalink_name] NVARCHAR(128) NOT NULL,
	[permalink_created] DATETIME NOT NULL,
	[permalink_updated] DATETIME NOT NULL,
	[permalink_created_by] UNIQUEIDENTIFIER NOT NULL,
	[permalink_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_permalink_id PRIMARY KEY ([permalink_id]),
	CONSTRAINT fk_permalink_namespace_id FOREIGN KEY ([permalink_namespace_id]) REFERENCES [namespace] ([namespace_id])
);
CREATE UNIQUE INDEX index_permalink_name ON [permalink] ([permalink_namespace_id], [permalink_name]);

CREATE TABLE [category] (
	[category_id] UNIQUEIDENTIFIER NOT NULL,
	[category_parent_id] UNIQUEIDENTIFIER NULL,
	[category_permalink_id] UNIQUEIDENTIFIER NOT NULL,
	[category_name] NVARCHAR(64) NOT NULL,
	[category_description] NVARCHAR(255) NULL,
	[category_created] DATETIME NOT NULL,
	[category_updated] DATETIME NOT NULL,
	[category_created_by] UNIQUEIDENTIFIER NOT NULL,
	[category_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_category_id PRIMARY KEY ([category_id]),
	CONSTRAINT fk_category_permalink_id FOREIGN KEY ([category_permalink_id]) REFERENCES [permalink] ([permalink_id]),
	CONSTRAINT fk_category_created_by FOREIGN KEY ([category_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_category_updated_by FOREIGN KEY ([category_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [relation] (
	[relation_id] UNIQUEIDENTIFIER NOT NULL,
	[relation_draft] BIT NOT NULL default(1),
	[relation_type] NVARCHAR(16) NOT NULL,
	[relation_data_id] UNIQUEIDENTIFIER NOT NULL,
	[relation_related_id] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_relation_id PRIMARY KEY ([relation_id], [relation_draft])
);

CREATE TABLE [page] (
	[page_id] UNIQUEIDENTIFIER NOT NULL,
	[page_draft] BIT NOT NULL default(1),
	[page_template_id] UNIQUEIDENTIFIER NOT NULL,
	[page_group_id] UNIQUEIDENTIFIER NULL,
	[page_group_disable_id] NTEXT NULL,
	[page_parent_id] UNIQUEIDENTIFIER NULL,
	[page_permalink_id] UNIQUEIDENTIFIER NOT NULL,
	[page_seqno] INT NOT NULL DEFAULT(1),
	[page_title] NVARCHAR(128) NOT NULL,
	[page_navigation_title] NVARCHAR(128) NULL,
	[page_is_hidden] BIT NOT NULL DEFAULT(0),
	[page_keywords] NVARCHAR(128) NULL,
	[page_description] NVARCHAR(255) NULL,
	[page_attachments] NTEXT NULL,
	[page_controller] NVARCHAR(128) NULL,
	[page_redirect] NVARCHAR(128) NULL,
	[page_created] DATETIME NOT NULL,
	[page_updated] DATETIME NOT NULL,
	[page_published] DATETIME NULL,
	[page_last_published] DATETIME NULL,
	[page_created_by] UNIQUEIDENTIFIER NOT NULL,
	[page_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_page_id PRIMARY KEY ([page_id], [page_draft]),
	CONSTRAINT fk_page_template_id FOREIGN KEY ([page_template_id]) REFERENCES [pagetemplate] ([pagetemplate_id]),
	CONSTRAINT fk_page_permalink_id FOREIGN KEY ([page_permalink_id]) REFERENCES [permalink] ([permalink_id]),
	CONSTRAINT fk_page_created_by FOREIGN KEY ([page_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_page_updated_by FOREIGN KEY ([page_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [regiontemplate] (
	[regiontemplate_id] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_template_id] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_internal_id] NVARCHAR(32) NOT NULL,
	[regiontemplate_seqno] INT NOT NULL default(1),
	[regiontemplate_name] NVARCHAR(64) NOT NULL,
	[regiontemplate_description] NVARCHAR(255) NULL,
	[regiontemplate_type] NVARCHAR(255) NOT NULL,
	[regiontemplate_created] DATETIME NOT NULL,
	[regiontemplate_updated] DATETIME NOT NULL,
	[regiontemplate_created_by] UNIQUEIDENTIFIER NOT NULL,
	[regiontemplate_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_regiontemplate_id PRIMARY KEY ([regiontemplate_id]),
	CONSTRAINT fk_regiontemplate_created_by FOREIGN KEY ([regiontemplate_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_regiontemplate_updated_by FOREIGN KEY ([regiontemplate_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);
CREATE UNIQUE INDEX [index_regiontemplate_internal_id] ON [regiontemplate] ([regiontemplate_template_id], [regiontemplate_internal_id]);

CREATE TABLE [region] (
	[region_id] UNIQUEIDENTIFIER NOT NULL,
	[region_draft] BIT NOT NULL default(1),
	[region_page_id] UNIQUEIDENTIFIER NULL,
	[region_page_draft] BIT NOT NULL default(1),
	[region_regiontemplate_id] UNIQUEIDENTIFIER NULL,
	[region_name] NVARCHAR(64) NULL,
	[region_body] NTEXT NULL,
	[region_created] DATETIME NOT NULL,
	[region_updated] DATETIME NOT NULL,
	[region_created_by] UNIQUEIDENTIFIER NOT NULL,
	[region_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_region_id PRIMARY KEY ([region_id], [region_draft]),
	CONSTRAINT fk_region_page_id FOREIGN KEY ([region_page_id], [region_page_draft]) REFERENCES [page] ([page_id], [page_draft]),
	CONSTRAINT fk_region_regiontemplate FOREIGN KEY ([region_regiontemplate_id]) REFERENCES [regiontemplate] ([regiontemplate_id]) ON DELETE CASCADE,
	CONSTRAINT fk_region_created_by FOREIGN KEY ([region_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_region_updated_by FOREIGN KEY ([region_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [property] (
	[property_id] UNIQUEIDENTIFIER NOT NULL,
	[property_draft] BIT NOT NULL default(1),
	[property_parent_id] UNIQUEIDENTIFIER NOT NULL,
	[property_name] NVARCHAR(64) NOT NULL,
	[property_value] NTEXT NULL,
	[property_created] DATETIME NOT NULL,
	[property_updated] DATETIME NOT NULL,
	[property_created_by] UNIQUEIDENTIFIER NOT NULL,
	[property_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_property_id PRIMARY KEY ([property_id], [property_draft]),
	CONSTRAINT fk_property_created_by FOREIGN KEY ([property_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_property_updated_by FOREIGN KEY ([property_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [post] (
	[post_id] UNIQUEIDENTIFIER NOT NULL,
	[post_draft] BIT NOT NULL default(1),
	[post_template_id] UNIQUEIDENTIFIER NOT NULL,
	[post_permalink_id] UNIQUEIDENTIFIER NOT NULL,
	[post_title] NVARCHAR(128) NOT NULL,
	[post_keywords] NVARCHAR(128) NULL,
	[post_description] NVARCHAR(255) NULL,
	[post_rss] BIT NOT NULL default(1),
	[post_excerpt] NVARCHAR(255) NULL,
	[post_body] NTEXT NULL,
	[post_attachments] NTEXT NULL,
	[post_controller] NVARCHAR(128) NULL,
	[post_archive_controller] NVARCHAR(128) NULL,
	[post_created] DATETIME NOT NULL,
	[post_updated] DATETIME NOT NULL,
	[post_published] DATETIME NULL,
	[post_last_published] DATETIME NULL,
	[post_created_by] UNIQUEIDENTIFIER NOT NULL,
	[post_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_post_id PRIMARY KEY ([post_id], [post_draft]),
	CONSTRAINT fk_post_template_id FOREIGN KEY ([post_template_id]) REFERENCES [posttemplate] ([posttemplate_id]),
	CONSTRAINT fk_post_permalink_id FOREIGN KEY ([post_permalink_id]) REFERENCES [permalink] ([permalink_id]),
	CONSTRAINT fk_post_created_by FOREIGN KEY ([post_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_post_updated_by FOREIGN KEY ([post_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [content] (
	[content_id] UNIQUEIDENTIFIER NOT NULL,
	[content_parent_id] UNIQUEIDENTIFIER NULL,
	[content_filename] NVARCHAR(128) NULL,
	[content_name] NVARCHAR(128) NULL,
	[content_type] NVARCHAR(255) NULL,
	[content_size] INT NOT NULL default(0),
	[content_image] BIT NOT NULL default(0),
	[content_folder] BIT NOT NULL default(0),
	[content_width] INT NULL,
	[content_height] INT NULL,
	[content_alt] NVARCHAR(128) NULL,
	[content_description] NVARCHAR(255) NULL,
	[content_created] DATETIME NOT NULL,
	[content_updated] DATETIME NOT NULL,
	[content_created_by] UNIQUEIDENTIFIER NOT NULL,
	[content_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_content_id PRIMARY KEY ([content_id]),
	CONSTRAINT fk_content_created_by FOREIGN KEY ([content_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_content_updated_by FOREIGN KEY ([content_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [upload] (
	[upload_id] UNIQUEIDENTIFIER NOT NULL,
	[upload_parent_id] UNIQUEIDENTIFIER NULL,
	[upload_filename] NVARCHAR(128) NOT NULL,
	[upload_type] NVARCHAR(255) NOT NULL,
	[upload_created] DATETIME NOT NULL,
	[upload_updated] DATETIME NOT NULL,
	[upload_created_by] UNIQUEIDENTIFIER NOT NULL,
	[upload_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_upload_id PRIMARY KEY ([upload_id]),
	CONSTRAINT fk_upload_created_by FOREIGN KEY ([upload_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_upload_updated_by FOREIGN KEY ([upload_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);

CREATE TABLE [extension] (
	[extension_id] UNIQUEIDENTIFIER NOT NULL,
	[extension_draft] BIT NOT NULL default(1),
	[extension_parent_id] UNIQUEIDENTIFIER NOT NULL,
	[extension_body] NTEXT NULL,
	[extension_type] NVARCHAR(255) NOT NULL,
	[extension_created] DATETIME NOT NULL,
	[extension_updated] DATETIME NOT NULL,
	[extension_created_by] UNIQUEIDENTIFIER NOT NULL,
	[extension_updated_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT pk_extension_id PRIMARY KEY ([extension_id], [extension_draft]),
	CONSTRAINT fk_extension_created_by FOREIGN KEY ([extension_created_by]) REFERENCES [sysuser] ([sysuser_id]),
	CONSTRAINT fk_extension_updated_by FOREIGN KEY ([extension_updated_by]) REFERENCES [sysuser] ([sysuser_id])
);
