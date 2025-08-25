select * from paymentdb.payment_transaction;
select * from paymentdb.payment_method_details;
select * from paymentdb.payment_transaction_log;
select * from paymentdb.refund_transaction;

INSERT INTO paymentdb.payment_transaction
(payment_reference, user_id, order_id, amount, currency, payment_method, status, status_message, created_at, completed_at)
VALUES
('PAY123456789', 'user_01', 1, 2500.00, 'INR', 'CreditCard', 'Success', 'Payment successful', NOW(), NOW());

INSERT INTO paymentdb.payment_method_details
(payment_transaction_id, card_holder_name, masked_card_number, wallet_id, bank_name,upi_id, card_type)
VALUES
(1, 'John Doe', '**** **** **** 1234', NULL, 'Axis Bank','1232323', '1');

INSERT INTO paymentdb.payment_transaction_log
(payment_transaction_id, timestamp, event_type, message)
VALUES
(1, NOW(), 'PaymentInitiated', 'Payment process started'),
(1, NOW(), 'PaymentSuccess', 'Payment completed successfully');

INSERT INTO paymentdb.refund_transaction
(payment_transaction_id, refund_amount, status, reason, requested_at, processed_at)
VALUES
(1, 2500.00, 'Success', 'Order canceled by user', NOW(), NOW());


