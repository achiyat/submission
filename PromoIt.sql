--drop table Associations
--drop table companies
--drop table Activists

--drop table campaignAsso
--drop table campaignActivist
--drop table campaignCompany
--drop table DonatedProducts
--drop table Shipments


--****יצירת טבלת עמותות****
create table Associations(
IDassn int primary key,
NameAssn varchar(30),
EmailAssn varchar(30),
)

--****יצירת טבלת חברות עסקיות****
create table companies(
IDCompany int primary key,
NameCompany varchar(30),
OwnerCompany varchar(30),
EmailCompany varchar(30),
PhoneCompany varchar(10)
)

--****יצירת טבלת פעילים****
create table Activists(
IDactivist int primary key,
NameActivist varchar(30),
EmailActivist varchar(30),
AddressActivist varchar(50),
phoneActivist varchar(10),
)

--****יצירת טבלת הודעות****
create table messages(
IDmessages int primary key identity,
UserId int,
FullName varchar(50),
Phone varchar(11),
Email varchar(50),
MessageUser varchar(300)
)

--****יצירת טבלת קמפיין****
create table campaignAsso(
IDcampaign int primary key identity,
NameCampaign varchar(30),

IDassn int,
NameAssn varchar(30),
EmailAssn varchar(30),
Fundraising int,

linkURL varchar(100),
Hashtag varchar(30),
SelectedCampaign bit,
StatusCampaign bit
FOREIGN KEY (IDassn) REFERENCES Associations(IDassn)
)

--****יצירת טבלת קמפיין חברה ****
create table campaignCompany(
IDcampaign int,
NameCampaign varchar(30),

IDassn int,
NameAssn varchar(30),
EmailAssn varchar(30),
Fundraising int,

linkURL varchar(100),
Hashtag varchar(30),
SelectedCampaign bit,
StatusCampaign bit,

IDCompany int,
NameCompany varchar(30),
OwnerCompany varchar(30),
EmailCompany varchar(30),
PhoneCompany varchar(10),
FOREIGN KEY (IDcampaign) REFERENCES campaignAsso(IDcampaign),
FOREIGN KEY (IDassn) REFERENCES Associations(IDassn),
FOREIGN KEY (IDCompany) REFERENCES companies(IDCompany)
)

--****יצירת טבלת קמפיין פעיל****
create table campaignActivist(
IDcampaign int,
NameCampaign varchar(30),

IDassn int,
NameAssn varchar(30),
EmailAssn varchar(30),

Fundraising int,
linkURL varchar(100),
Hashtag varchar(30),
SelectedCampaign bit,
StatusCampaign bit,

IDactivist int,
NameActivist varchar(30),
EmailActivist varchar(30),
AddressActivist varchar(50),
phoneActivist varchar(10),
MoneyActivist int,
FOREIGN KEY (IDcampaign) REFERENCES campaignAsso(IDcampaign),
FOREIGN KEY (IDassn) REFERENCES Associations(IDassn),
FOREIGN KEY (IDactivist) REFERENCES Activists(IDactivist)
)

--****יצירת טבלת מוצרים שנתרמו****
create table DonatedProducts(
IDProduct int primary key identity,
ProductName varchar(20),
Price int,
Inventory int,
SelectedProduct bit,
StatusProduct bit,

IDcampaign int,
NameCampaign varchar(30),
IDassn int,
NameAssn varchar(30),
EmailAssn varchar(30),
Fundraising int,
linkURL varchar(100),
Hashtag varchar(30),
SelectedCampaign bit,
StatusCampaign bit,

IDCompany int,
NameCompany varchar(30),
OwnerCompany varchar(30),
EmailCompany varchar(30),
PhoneCompany varchar(10),


FOREIGN KEY (IDcampaign) REFERENCES campaignAsso(IDcampaign),
FOREIGN KEY (IDassn) REFERENCES Associations(IDassn),
FOREIGN KEY (IDCompany) REFERENCES companies(IDCompany)
)

--****יצירת טבלת משלוחים****

create table Shipments(
IDShipments int primary key identity,
donated bit,
bought bit,

IDProduct int,
ProductName varchar(20),
Price int,
Inventory int,
SelectedProduct bit,
StatusProduct bit,

IDcampaign int,
NameCampaign varchar(30),
IDassn int,
NameAssn varchar(30),
EmailAssn varchar(30),
Fundraising int,
linkURL varchar(100),
Hashtag varchar(30),
SelectedCampaign bit,
StatusCampaign bit,

IDCompany int,
NameCompany varchar(30),
OwnerCompany varchar(30),
EmailCompany varchar(30),
PhoneCompany varchar(10),


IDactivist int,
NameActivist varchar(30),
EmailActivist varchar(30),
AddressActivist varchar(50),
phoneActivist varchar(10),
MoneyActivist int,

FOREIGN KEY (IDProduct) REFERENCES DonatedProducts(IDProduct),
FOREIGN KEY (IDcampaign) REFERENCES campaignAsso(IDcampaign),
FOREIGN KEY (IDassn) REFERENCES Associations(IDassn),
FOREIGN KEY (IDCompany) REFERENCES companies(IDCompany),
FOREIGN KEY (IDactivist) REFERENCES Activists(IDactivist)
)

select * from messages
select * from companies
select * from Associations
select * from Activists

select * from campaignAsso
select * from campaignActivist
select * from campaignCompany
select * from DonatedProducts
select * from Shipments
