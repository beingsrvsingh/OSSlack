use addressdb;

INSERT INTO address_type (id, name, description)
VALUES
(1, 'Office', 'Office or workplace address'),
(2, 'Home', 'Personal or residential address')


INSERT INTO addressdb.address (
    uid, user_id, owner_type_id, name,
    address_line_1, address_line_2, city, state, country, pincode,
    landmark, phone_number, address_type_id, is_default, is_active,
    latitude, longitude, time_zone, created_by, updated_by, created_at, updated_at
)
VALUES
(
    UUID(),                       -- unique UID
    1,                         -- OwnerId
    1,                            -- OwnerType = User
    'Saurav Sinha',
    'Gate No.7 7005 - A FirstFloor',
    'Kailash Colony',
    'New Delhi',
    'Delhi',
    'India',
    '110066',
    'Near Main Market',
    '981806003',
    1,                            -- AddressType = Home
    TRUE,                         -- IsDefault
    TRUE,                         -- IsActive
    NULL,                         -- Latitude
    NULL,                         -- Longitude
    'Asia/Kolkata',               -- TimeZone
    1,                            -- CreatedBy
    1,                            -- UpdatedBy
    NOW(6),
    NOW(6)
),
(
    UUID(),
    1,
    1,
    'Jaksh Sinha',
    'Gate No.9007 - A Fourth',
    'East of Kailash',
    'Lajpat Nagar',
    'Delhi',
    'India',
    '110055',
    'Near Metro Station',
    '801806995',
    2,                            -- AddressType = Office
    FALSE,
    TRUE,
    NULL,
    NULL,
    'Asia/Kolkata',
    1,
    1,
    NOW(6),
    NOW(6)
);



select * from address;
select * from address_type;


