| Id   | Name                          | Description                     | Price   | ImageUrl                   | SKU     | ProductType | IsNew | IsFeatured | CreatedAt           | UpdatedAt | SubCategoryId | SubCategoryNameSnapshot | CategoryNameSnapshot |
| ---- | ----------------------------- | ------------------------------- | ------- | -------------------------- | ------- | ----------- | ----- | ---------- | ------------------- | --------- | ------------- | ----------------------- | -------------------- |
| 1000 | Agarbatti Pack                | Pack of 10 incense sticks       | 50.00   | /images/agarbatti.jpg      | AGAR123 | Product     | true  | true       | 2024-08-13 10:00:00 | NULL      | 400           | Incense Sticks          | Products             |
| 1001 | Ganesh Pooja Kit              | Complete Ganesh pooja kit       | 1200.00 | /images/ganeshkit.jpg      | KIT001  | Kit         | false | true       | 2024-08-10 09:30:00 | NULL      | 500           | Ganesh Pooja Kit        | Kits                 |
| 1002 | Spiritual Book: Bhagavad Gita | Classic religious book          | 200.00  | /images/bgita.jpg          | BK1001  | Product     | true  | false      | 2024-08-05 11:00:00 | NULL      | 600           | Spiritual Books         | Products             |
| 1003 | Decorative Lamp               | Traditional oil lamp decoration | 350.00  | /images/lamp.jpg           | LAMP101 | Product     | false | false      | 2024-07-30 14:00:00 | NULL      | 401           | Diyas & Lamps           | Products             |
| 1004 | Navagraha Pooja Kit           | Complete Navagraha pooja kit    | 1100.00 | /images/navkit.jpg         | KIT002  | Kit         | true  | false      | 2024-08-11 08:30:00 | NULL      | 501           | Navagraha Pooja Kit     | Kits                 |
| 1005 | Incense Stick Variant A       | Lavender scent incense sticks   | 55.00   | /images/agarbatti\_lav.jpg | AGAR124 | Product     | true  | false      | 2024-08-12 15:20:00 | NULL      | 400           | Incense Sticks          | Products             |



| Id | ProductId | Region | Price   |
| -- | --------- | ------ | ------- |
| 1  | 1000      | US     | 55.00   |
| 2  | 1000      | India  | 50.00   |
| 3  | 1001      | US     | 1250.00 |
| 4  | 1002      | India  | 190.00  |
| 5  | 1004      | UK     | 1150.00 |


| Id | ProductId | VariantName | VariantValue |
| -- | --------- | ----------- | ------------ |
| 1  | 1005      | Scent       | Lavender     |
| 2  | 1005      | Size        | Small        |
| 3  | 1003      | Color       | Brass        |


| Id | ProductId | AttributeName | AttributeValue           |
| -- | --------- | ------------- | ------------------------ |
| 1  | 1000      | Material      | Bamboo sticks            |
| 2  | 1002      | Author        | Various                  |
| 3  | 1003      | Weight        | 500 grams                |
| 4  | 1001      | Kit Includes  | Agarbatti, lamp, flowers |


| Id | ProductId | LanguageCode | LocalizedName   | LocalizedDescription            |
| -- | --------- | ------------ | --------------- | ------------------------------- |
| 1  | 1000      | en-US        | Agarbatti Pack  | Pack of 10 incense sticks       |
| 2  | 1000      | hi-IN        | अगरबत्ती पैक    | 10 अगरबत्तियों का पैक           |
| 3  | 1002      | en-US        | Bhagavad Gita   | Classic religious book          |
| 4  | 1003      | en-US        | Decorative Lamp | Traditional oil lamp decoration |


| Id | ProductId | Tag            |
| -- | --------- | -------------- |
| 1  | 1000      | incense        |
| 2  | 1001      | pooja kit      |
| 3  | 1002      | spiritual book |
| 4  | 1003      | lamp           |


| Id   | MetaTitle               | MetaDescription                       | MetaKeywords                    |
| ---- | ----------------------- | ------------------------------------- | ------------------------------- |
| 1000 | Buy Agarbatti Pack      | Buy quality incense sticks online     | agarbatti, incense, pooja items |
| 1001 | Ganesh Pooja Kit Online | Complete Ganesh pooja kit for rituals | ganesh, pooja kit, spiritual    |
| 1002 | Bhagavad Gita Book      | Authentic spiritual book for all ages | bhagavad gita, spiritual book   |
