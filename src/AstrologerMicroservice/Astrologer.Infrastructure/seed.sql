ALTER TABLE astrologer_entity ENGINE = InnoDB;

-- On astrologer_entity.name
ALTER TABLE astrologer_entity
ADD FULLTEXT INDEX ix_astrologer_entity_name (name);

-- On astrologer_expertises.name (package name)
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_name (name);

-- On astrologer_expertises.category_name_snap
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_cat_snap (category_name_snap);

-- On astrologer_expertises.sub_cat_name_snap
ALTER TABLE astrologer_expertises
ADD FULLTEXT INDEX ix_astrologer_expertises_subcat_snap (sub_cat_name_snap);
