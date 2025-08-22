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