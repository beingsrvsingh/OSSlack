select * from reviewdb.reviews;
select * from reviewdb.Review_Feedbacks;
select * from reviewdb.Review_Media;
select * from reviewdb.review_reports;

-- Insert a Review
INSERT INTO reviewdb.reviews (
    Id, Product_Id, User_Id, created_by, Rating, Title, `Comment`, Helpful_Count, Unhelpful_Count,
    Created_At, Updated_At, `Status`, Moderation_Comment, moderated_by_user_id, Moderated_At
)
VALUES 
(
    1, 1, 'user123', 'Saurav Sinha', 5, 'Great product!', 'I loved using this. Highly recommend.',
    3, 1, NOW(), NULL, 1, NULL, NULL, NULL
);,
(
    1, 2, 'user123', 'Neeraj Pandey', 5, 'Great product!', 'I loved using this. Highly recommend.',
    3, 1, NOW(), NULL, 1, NULL, NULL, NULL
);

-- Insert Review Feedback
INSERT INTO reviewdb.Review_Feedbacks (Id, Review_Id, User_Id, Is_Helpful, Created_At)
VALUES 
(1, 1, 'user456', 1, NOW()),
(2, 1, 'user789', 1, NOW()),
(3, 1, 'user321', 0, NOW());

-- Insert Review Media
INSERT INTO reviewdb.Review_Media (Id, Review_Id, Url, `Type`)
VALUES 
(1, 1, 'https://example.com/image1.jpg', 0),  -- MediaType.Image
(2, 1, 'https://example.com/image2.jpg', 0);

-- Insert Review Report
INSERT INTO reviewdb.review_reports (Id, Review_Id, Reported_By_User_Id, Reason, Reported_At, 
`Status`, Resolution_Note, Resolved_At, Resolved_By_User_Id)
VALUES 
(1, 1, 'user888', 'Inappropriate language', NOW(), 0, NULL, NULL, NULL);
