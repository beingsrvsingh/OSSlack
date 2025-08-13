| Id | UserId                                 | DisplayName        | ProfilePictureUrl                    | AverageRating | TotalRatings | IsActive | CreatedAt            | UpdatedAt | ConsultationModes |
| -- | -------------------------------------- | ------------------ | ------------------------------------ | ------------- | ------------ | -------- | -------------------- | --------- | ----------------- |
| 1  | `c12e4f2e-a45f-42f9-a531-11cdef12345a` | Acharya Ram        | /images/astrologers/ram.jpg          | 4.5           | 120          | true     | 2025-08-13T10:00:00Z | null      | Chat, Call        |
| 2  | `f33e9ba1-d6ab-42dd-9053-bc27adf3e2b1` | Pandit Vishal      | /images/astrologers/vishal.jpg       | 4.8           | 240          | true     | 2025-08-13T10:05:00Z | null      | Call, Video       |
| 3  | `afd8c531-91f5-489e-86fd-a12091e1ff42` | Jyotishi Meera     | /images/astrologers/meera.jpg        | 4.3           | 90           | true     | 2025-08-13T10:10:00Z | null      | Chat              |
| 4  | `3beabeb5-bb0b-4c60-bac4-ff9d1e8127c5` | Guru Mahadev       | /images/astrologers/mahadev.jpg      | 4.9           | 310          | true     | 2025-08-13T10:15:00Z | null      | Chat, Call, Video |
| 5  | `e420a3e6-0f6e-4b7a-8319-3aafe12345cd` | Swami Krishnaanand | /images/astrologers/krishnaanand.jpg | 4.7           | 180          | true     | 2025-08-13T10:20:00Z | null      | Call              |


| Id | AstrologerId | Language |
| -- | ------------ | -------- |
| 1  | 1            | Hindi    |
| 2  | 1            | Sanskrit |
| 3  | 2            | Hindi    |
| 4  | 2            | English  |
| 5  | 3            | Tamil    |
| 6  | 4            | Hindi    |
| 7  | 5            | Hindi    |
| 8  | 5            | Kannada  |


| Id | AstrologerId | ExpertiseType        |
| -- | ------------ | -------------------- |
| 1  | 1            | Horoscope            |
| 2  | 1            | Marriage Matching    |
| 3  | 2            | Palmistry            |
| 4  | 2            | Tarot Reading        |
| 5  | 3            | Career Guidance      |
| 6  | 4            | Vaastu               |
| 7  | 4            | Kundli Matching      |
| 8  | 5            | Spiritual Counseling |


| Id | AstrologerId | DayOfWeek | StartTime | EndTime |
| -- | ------------ | --------- | --------- | ------- |
| 1  | 1            | Monday    | 09:00     | 13:00   |
| 2  | 1            | Wednesday | 10:00     | 14:00   |
| 3  | 2            | Tuesday   | 11:00     | 15:00   |
| 4  | 3            | Friday    | 13:00     | 17:00   |


| Id | ScheduleId | Date       | StartTime | EndTime | IsBooked |
| -- | ---------- | ---------- | --------- | ------- | -------- |
| 1  | 1          | 2025-08-15 | 09:00     | 09:30   | false    |
| 2  | 1          | 2025-08-15 | 09:30     | 10:00   | true     |
| 3  | 2          | 2025-08-17 | 10:00     | 10:30   | false    |
