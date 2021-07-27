﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo.blog (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    name varchar NOT NULL,
    site_link varchar NOT NULL,
    secret varchar NULL,
    CONSTRAINT "PK_Blog" PRIMARY KEY (id)
);

CREATE TABLE dbo.identityrole (
    id varchar NOT NULL,
    name varchar NULL,
    normalized_name varchar NULL,
    concurrency_stamp varchar NULL,
    CONSTRAINT pk_identityrole PRIMARY KEY (id)
);

CREATE TABLE dbo.identityuser (
    id varchar NOT NULL,
    user_name varchar NULL,
    normalized_user_name varchar NULL,
    email varchar NULL,
    normalized_email varchar NULL,
    email_confirmed boolean NOT NULL,
    password_hash varchar NULL,
    security_stamp varchar NULL,
    concurrency_stamp varchar NULL,
    phone_number varchar NULL,
    phone_number_confirmed boolean NOT NULL,
    two_factor_enabled boolean NOT NULL,
    lockout_end timestamp with time zone NULL,
    lockout_enabled boolean NOT NULL,
    access_failed_count integer NOT NULL,
    CONSTRAINT pk_identityuser PRIMARY KEY (id)
);

CREATE TABLE dbo.blog_article (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    blog_id1 integer NOT NULL,
    blog_id integer NULL,
    link varchar NOT NULL,
    image varchar NOT NULL,
    likes integer NOT NULL,
    dislikes integer NOT NULL,
    tittle varchar NOT NULL,
    CONSTRAINT "PK_Blog_Article" PRIMARY KEY (id),
    CONSTRAINT fk_blog_article_blog_blog_id FOREIGN KEY (blog_id) REFERENCES dbo.blog (id) ON DELETE RESTRICT
);

CREATE TABLE dbo."identityroleclaim<string>" (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    role_id varchar NOT NULL,
    claim_type varchar NULL,
    claim_value varchar NULL,
    CONSTRAINT pk_identityroleclaim_string PRIMARY KEY (id),
    CONSTRAINT fk_identityroleclaim_string_identityrole_role_id FOREIGN KEY (role_id) REFERENCES dbo.identityrole (id) ON DELETE CASCADE
);

CREATE TABLE dbo.blog_user (
    id varchar NOT NULL,
    birth_day timestamp without time zone NOT NULL,
    CONSTRAINT pk_identityuser PRIMARY KEY (id),
    CONSTRAINT fk_blog_user_identityuser_id FOREIGN KEY (id) REFERENCES dbo.identityuser (id) ON DELETE CASCADE
);

CREATE TABLE dbo."identityuserclaim<string>" (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    user_id varchar NOT NULL,
    claim_type varchar NULL,
    claim_value varchar NULL,
    CONSTRAINT pk_identityuserclaim_string PRIMARY KEY (id),
    CONSTRAINT fk_identityuserclaim_string_identityuser_user_id FOREIGN KEY (user_id) REFERENCES dbo.identityuser (id) ON DELETE CASCADE
);

CREATE TABLE dbo."identityuserlogin<string>" (
    login_provider varchar NOT NULL,
    provider_key varchar NOT NULL,
    provider_display_name varchar NULL,
    user_id varchar NOT NULL,
    CONSTRAINT pk_identityuserlogin_string PRIMARY KEY (login_provider, provider_key),
    CONSTRAINT fk_identityuserlogin_string_identityuser_user_id FOREIGN KEY (user_id) REFERENCES dbo.identityuser (id) ON DELETE CASCADE
);

CREATE TABLE dbo."identityuserrole<string>" (
    user_id varchar NOT NULL,
    role_id varchar NOT NULL,
    CONSTRAINT pk_identityuserrole_string PRIMARY KEY (user_id, role_id),
    CONSTRAINT fk_identityuserrole_string_identityrole_role_id FOREIGN KEY (role_id) REFERENCES dbo.identityrole (id) ON DELETE CASCADE,
    CONSTRAINT fk_identityuserrole_string_identityuser_user_id FOREIGN KEY (user_id) REFERENCES dbo.identityuser (id) ON DELETE CASCADE
);

CREATE TABLE dbo."identityusertoken<string>" (
    user_id varchar NOT NULL,
    login_provider varchar NOT NULL,
    name varchar NOT NULL,
    value varchar NULL,
    CONSTRAINT pk_identityusertoken_string PRIMARY KEY (user_id, login_provider, name),
    CONSTRAINT fk_identityusertoken_string_identityuser_user_id FOREIGN KEY (user_id) REFERENCES dbo.identityuser (id) ON DELETE CASCADE
);

CREATE TABLE dbo.blog_article_comment (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    article_id1 integer NOT NULL,
    article_id integer NULL,
    comment varchar NOT NULL,
    published timestamp without time zone NOT NULL,
    ip_address varchar NOT NULL,
    CONSTRAINT "PK_Blog_Article_Comment" PRIMARY KEY (id),
    CONSTRAINT fk_blog_article_comment_blog_article_article_id FOREIGN KEY (article_id) REFERENCES dbo.blog_article (id) ON DELETE RESTRICT
);

CREATE INDEX ix_blog_article_blog_id ON dbo.blog_article (blog_id);

CREATE INDEX ix_blog_article_comment_article_id ON dbo.blog_article_comment (article_id);

CREATE UNIQUE INDEX "RoleNameIndex" ON dbo.identityrole (normalized_name);

CREATE INDEX ix_identityroleclaim_string_role_id ON dbo."identityroleclaim<string>" (role_id);

CREATE INDEX "EmailIndex" ON dbo.identityuser (normalized_email);

CREATE UNIQUE INDEX "UserNameIndex" ON dbo.identityuser (normalized_user_name);

CREATE INDEX ix_identityuserclaim_string_user_id ON dbo."identityuserclaim<string>" (user_id);

CREATE INDEX ix_identityuserlogin_string_user_id ON dbo."identityuserlogin<string>" (user_id);

CREATE INDEX ix_identityuserrole_string_role_id ON dbo."identityuserrole<string>" (role_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20210722154912_JWTImplementation', '5.0.8');

COMMIT;
