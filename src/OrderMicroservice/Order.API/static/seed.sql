INSERT INTO orderdb.order_header (
    id, order_number, user_id, customer_id, address_id, status, order_date, shipped_date, delivered_date, estimated_delivery_date,
    shipping_method, tracking_number, total_amount, tax_amount, shipping_fee, platform_fee, surge_fee,
    discount_amount, currency_code, is_gift, gift_message, customer_notes, admin_notes, refund_status,
    created_at, updated_at, created_by, updated_by, delivery_partner_user_id, delivery_partner_name
) VALUES (
    1, 'ORD10001', 'user_123', 101, 1, '0', UTC_TIMESTAMP(6), NULL, NULL, NULL,
    'Standard', 'TRK123456', 1000.00, 80.00, 50.00, 10.00, 0.00,
    150.00, 'USD', FALSE, NULL, 'Please handle with care', NULL, 'None',
    UTC_TIMESTAMP(6), NULL, 'admin', NULL, NULL, NULL
);


INSERT INTO orderdb.order_item (
    id, order_header_id, product_id, product_type, product_url, product_name, sku, quantity,
    unit_price, tax_amount, discount_amount, total_price, weight_value, weight_unit,
    product_options, fulfillment_status, return_status, customer_notes,
    created_at, updated_at, created_by, updated_by
) VALUES (
    1, 1, 501, '3', 'https://example.com/product-image.jpg', 'MatchBox', 'MBX-001', 2,
    500.00, 80.00, 100.00, 980.00, 0.5000, 'kg',
    '{"color":"red"}', 'Pending', 'None', 'Gift wrap requested',
    UTC_TIMESTAMP(6), NULL, 'admin', NULL
);

select * from orderdb.order_header;
select * from orderdb.order_item;
