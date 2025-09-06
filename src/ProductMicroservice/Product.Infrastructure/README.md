dotnet ef migrations add Initial-Create --output-dir Migrations --context ProductDbContext --startup-project ../Product.API 

dotnet ef database update --context ProductDbContext --startup-project ../Product.API

SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE productdb.product_attribute_value;
TRUNCATE TABLE productdb.product_seo_info_master;
TRUNCATE TABLE productdb.product_tag_master;
TRUNCATE TABLE productdb.localized_product_info_master;
TRUNCATE TABLE productdb.product_variant_master;
TRUNCATE TABLE productdb.product_region_price_master;
TRUNCATE TABLE productdb.product_master;

SET FOREIGN_KEY_CHECKS = 1;

# PRE-REQUISITE

    -   Adds a FULLTEXT index on the name column of the product_master table.

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        ALTER TABLE product_master ADD FULLTEXT index_ft_name (name);
        ALTER TABLE product_master ADD FULLTEXT index_ft_cat_snap (cat_snap);
        ALTER TABLE product_master ADD FULLTEXT index_ft_subcat_snap (subcat_snap);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        ALTER TABLE product_master DROP INDEX index_ft_name;
        ALTER TABLE product_master DROP INDEX index_ft_cat_snap;
        ALTER TABLE product_master DROP INDEX index_ft_subcat_snap;

    }
