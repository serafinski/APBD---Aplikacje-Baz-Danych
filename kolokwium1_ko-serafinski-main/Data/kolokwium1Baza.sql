-- tables
-- Table: Client
CREATE TABLE CLIENT (
                        ID INT NOT NULL IDENTITY,
                        FIRSTNAME NVARCHAR(50) NOT NULL,
                        LASTNAME NVARCHAR(100) NOT NULL,
                        CONSTRAINT CLIENT_PK PRIMARY KEY (ID)
);

-- Table: Order
CREATE TABLE "Order" (
                         ID INT NOT NULL IDENTITY,
                         CREATEDAT DATE NOT NULL,
                         CLIENT_ID INT NOT NULL,
                         STATUS_ID INT NOT NULL,
                         CONSTRAINT ORDER_PK PRIMARY KEY (ID)
);

-- Table: Product
CREATE TABLE PRODUCT (
                         ID INT NOT NULL IDENTITY,
                         NAME NVARCHAR(50) NOT NULL,
                         PRICE NUMERIC(10, 2) NOT NULL,
                         CONSTRAINT PRODUCT_PK PRIMARY KEY (ID)
);

-- Table: Product_Order
CREATE TABLE PRODUCT_ORDER (
                               PRODUCT_ID INT NOT NULL,
                               ORDER_ID INT NOT NULL,
                               AMOUNT INT NOT NULL,
                               CONSTRAINT PRODUCT_ORDER_PK PRIMARY KEY (PRODUCT_ID, ORDER_ID)
);

-- Table: Status
CREATE TABLE STATUS (
                        ID INT NOT NULL IDENTITY,
                        NAME NVARCHAR(50) NOT NULL,
                        CONSTRAINT STATUS_PK PRIMARY KEY (ID)
);

-- foreign keys
-- Reference: Order_Status (table: Order)
ALTER TABLE "Order" ADD CONSTRAINT ORDER_STATUS FOREIGN KEY (STATUS_ID) REFERENCES STATUS (ID);

-- Reference: Product_Order_Order (table: Product_Order)
ALTER TABLE PRODUCT_ORDER ADD CONSTRAINT PRODUCT_ORDER_ORDER FOREIGN KEY (ORDER_ID) REFERENCES "Order" (ID);

-- Reference: Product_Order_Product (table: Product_Order)
ALTER TABLE PRODUCT_ORDER ADD CONSTRAINT PRODUCT_ORDER_PRODUCT FOREIGN KEY (PRODUCT_ID) REFERENCES PRODUCT (ID);

-- Reference: Zamowienie_Client (table: Order)
ALTER TABLE "Order" ADD CONSTRAINT ZAMOWIENIE_CLIENT FOREIGN KEY (CLIENT_ID) REFERENCES CLIENT (ID);

-- End of file.

INSERT INTO STATUS (
    NAME
) VALUES (
             'Created'
         );

INSERT INTO STATUS (
    NAME
) VALUES (
             'Ongoing'
         );

INSERT INTO STATUS (
    NAME
) VALUES (
             'Finished'
         );

INSERT INTO PRODUCT (
    NAME,
    PRICE
) VALUES (
             'Chef Hat 25cm',
             81.07
         );

INSERT INTO PRODUCT (
    NAME,
    PRICE
) VALUES (
             'Curry Powder Madras',
             40.65
         );

INSERT INTO PRODUCT (
    NAME,
    PRICE
) VALUES (
             'Flour - Semolina',
             59.28
         );

INSERT INTO PRODUCT (
    NAME,
    PRICE
) VALUES (
             'Wine - Tio Pepe Sherry Fino',
             4.44
         );

INSERT INTO PRODUCT (
    NAME,
    PRICE
) VALUES (
             'Beets - Mini Golden',
             89.66
         );

INSERT INTO CLIENT (
    FIRSTNAME,
    LASTNAME
) VALUES (
             'Jerad',
             'Plummer'
         );

INSERT INTO CLIENT (
    FIRSTNAME,
    LASTNAME
) VALUES (
             'Austin',
             'Maryska'
         );

INSERT INTO CLIENT (
    FIRSTNAME,
    LASTNAME
) VALUES (
             'Emalia',
             'Kilian'
         );

INSERT INTO CLIENT (
    FIRSTNAME,
    LASTNAME
) VALUES (
             'Mitchael',
             'Piatkow'
         );

INSERT INTO CLIENT (
    FIRSTNAME,
    LASTNAME
) VALUES (
             'Granger',
             'Blackaller'
         );

INSERT INTO "Order" (
    CREATEDAT,
    CLIENT_ID,
    STATUS_ID
) VALUES (
             '2023-04-16',
             1,
             1
         );

INSERT INTO "Order" (
    CREATEDAT,
    CLIENT_ID,
    STATUS_ID
) VALUES (
             '2023-04-17',
             1,
             3
         );

INSERT INTO "Order" (
    CREATEDAT,
    CLIENT_ID,
    STATUS_ID
) VALUES (
             '2023-04-19',
             2,
             2
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             1,
             1,
             3
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             3,
             1,
             8
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             2,
             1,
             4
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             3,
             2,
             7
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             4,
             2,
             2
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             1,
             2,
             13
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             5,
             3,
             14
         );

INSERT INTO PRODUCT_ORDER (
    PRODUCT_ID,
    ORDER_ID,
    AMOUNT
) VALUES (
             4,
             3,
             1
         );