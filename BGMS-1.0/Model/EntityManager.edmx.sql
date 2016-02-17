



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/08/2015 12:51:55
-- Generated from EDMX file: C:\Users\Ganjaboi\Documents\Visual Studio 2012\Projects\BGMS-1.0\BGMS-1.0\Model\EntityManager.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `adjustments` DROP CONSTRAINT `FK_adjustmentsstock`;
--    ALTER TABLE `collectioncheckitems` DROP CONSTRAINT `FK_collectioncheckcustomercollection`;
--    ALTER TABLE `collectionorderitems` DROP CONSTRAINT `FK_collectionsalesordercustomercollection`;
--    ALTER TABLE `collectionorderitems` DROP CONSTRAINT `FK_collectionsalesordersalesorder`;
--    ALTER TABLE `customers` DROP CONSTRAINT `FK_CustomerAgent`;
--    ALTER TABLE `customercollections` DROP CONSTRAINT `FK_customercollectioncustomer`;
--    ALTER TABLE `paymentorderitems` DROP CONSTRAINT `FK_paymentorderitempurchaseorder`;
--    ALTER TABLE `paymentorderitems` DROP CONSTRAINT `FK_paymentorderitemsupplierpayment`;
--    ALTER TABLE `purchaseorderitems` DROP CONSTRAINT `FK_PurchaseOrderDetails1`;
--    ALTER TABLE `purchaseorderitems` DROP CONSTRAINT `FK_PurchaseOrderItemStock1`;
--    ALTER TABLE `purchaseorders` DROP CONSTRAINT `FK_PurchaseOrderSupplier`;
--    ALTER TABLE `purchasereturnitems` DROP CONSTRAINT `FK_purchasereturnitempurchasereturn`;
--    ALTER TABLE `purchasereturnitems` DROP CONSTRAINT `FK_purchasereturnitemstock`;
--    ALTER TABLE `purchasereturns` DROP CONSTRAINT `FK_purchasereturnsupplier`;
--    ALTER TABLE `salesorders` DROP CONSTRAINT `FK_salesorderagent`;
--    ALTER TABLE `salesorders` DROP CONSTRAINT `FK_salesordercustomer`;
--    ALTER TABLE `salesorderitems` DROP CONSTRAINT `FK_salesorderitemstock`;
--    ALTER TABLE `salesorderitems` DROP CONSTRAINT `FK_salesordersalesorderitem`;
--    ALTER TABLE `salesreturns` DROP CONSTRAINT `FK_salesreturnagent`;
--    ALTER TABLE `salesreturns` DROP CONSTRAINT `FK_salesreturncustomer`;
--    ALTER TABLE `salesreturnitems` DROP CONSTRAINT `FK_salesreturnitemsalesreturn`;
--    ALTER TABLE `salesreturnitems` DROP CONSTRAINT `FK_salesreturnitemstock`;
--    ALTER TABLE `stocks` DROP CONSTRAINT `FK_StockCategory`;
--    ALTER TABLE `stocks` DROP CONSTRAINT `FK_StockUnit`;
--    ALTER TABLE `paymentcheckitems` DROP CONSTRAINT `FK_supplierpaymentpaymentcheckitem`;
--    ALTER TABLE `supplierpayments` DROP CONSTRAINT `FK_supplierpaymentsupplier`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `activities`;
    DROP TABLE IF EXISTS `adjustments`;
    DROP TABLE IF EXISTS `agents`;
    DROP TABLE IF EXISTS `categories`;
    DROP TABLE IF EXISTS `collectioncheckitems`;
    DROP TABLE IF EXISTS `collectionorderitems`;
    DROP TABLE IF EXISTS `counters`;
    DROP TABLE IF EXISTS `customercollections`;
    DROP TABLE IF EXISTS `customers`;
    DROP TABLE IF EXISTS `paymentcheckitems`;
    DROP TABLE IF EXISTS `paymentorderitems`;
    DROP TABLE IF EXISTS `purchaseorderitems`;
    DROP TABLE IF EXISTS `purchaseorders`;
    DROP TABLE IF EXISTS `purchasereturnitems`;
    DROP TABLE IF EXISTS `purchasereturns`;
    DROP TABLE IF EXISTS `salesorderitems`;
    DROP TABLE IF EXISTS `salesorders`;
    DROP TABLE IF EXISTS `salesreturnitems`;
    DROP TABLE IF EXISTS `salesreturns`;
    DROP TABLE IF EXISTS `stocks`;
    DROP TABLE IF EXISTS `supplierpayments`;
    DROP TABLE IF EXISTS `suppliers`;
    DROP TABLE IF EXISTS `units`;
    DROP TABLE IF EXISTS `users`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `activities`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Description` varchar (255) NOT NULL, 
	`Date` datetime NOT NULL);

ALTER TABLE `activities` ADD PRIMARY KEY (Id);




CREATE TABLE `agents`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`Address` longtext, 
	`Contact` longtext, 
	`Tin` longtext, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`Active` bool NOT NULL);

ALTER TABLE `agents` ADD PRIMARY KEY (Id);




CREATE TABLE `categories`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`Active` bool NOT NULL, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime);

ALTER TABLE `categories` ADD PRIMARY KEY (Id);




CREATE TABLE `collectioncheckitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` longtext NOT NULL, 
	`Date` datetime NOT NULL, 
	`Amount` double NOT NULL, 
	`customercollectionId` int NOT NULL);

ALTER TABLE `collectioncheckitems` ADD PRIMARY KEY (Id);




CREATE TABLE `collectionorderitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`salesorderId` int NOT NULL, 
	`Amount` double NOT NULL, 
	`customercollectionId` int NOT NULL, 
	`BalanceGot` double NOT NULL);

ALTER TABLE `collectionorderitems` ADD PRIMARY KEY (Id);




CREATE TABLE `counters`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (255) NOT NULL, 
	`Count` int NOT NULL, 
	`Prefix` varchar (10), 
	`Owner` varchar (255) NOT NULL, 
	`Active` bool NOT NULL);

ALTER TABLE `counters` ADD PRIMARY KEY (Id);




CREATE TABLE `customercollections`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`PostedDate` datetime, 
	`Remarks` longtext NOT NULL, 
	`TotalCheck` double NOT NULL, 
	`TotalPaid` double NOT NULL, 
	`ModifyBy` longtext NOT NULL, 
	`ModifyDate` datetime NOT NULL, 
	`customerId` int NOT NULL, 
	`Bank` longtext NOT NULL, 
	`DocumentNo` varchar (255) NOT NULL);

ALTER TABLE `customercollections` ADD PRIMARY KEY (Id);




CREATE TABLE `customers`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`Address` longtext, 
	`Contact` longtext, 
	`Fax` longtext, 
	`Tin` longtext, 
	`Commission` double, 
	`AgentId` int, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`Active` bool NOT NULL);

ALTER TABLE `customers` ADD PRIMARY KEY (Id);




CREATE TABLE `paymentcheckitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` longtext NOT NULL, 
	`Date` datetime NOT NULL, 
	`Amount` double NOT NULL, 
	`supplierpaymentId` int NOT NULL);

ALTER TABLE `paymentcheckitems` ADD PRIMARY KEY (Id);




CREATE TABLE `paymentorderitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Amount` double NOT NULL, 
	`purchaseorderId` int NOT NULL, 
	`supplierpaymentId` int NOT NULL, 
	`BalanceGot` double NOT NULL);

ALTER TABLE `paymentorderitems` ADD PRIMARY KEY (Id);




CREATE TABLE `purchaseorderitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Quantity` int NOT NULL, 
	`Price` double NOT NULL, 
	`stockId` int NOT NULL, 
	`PurchaseOrderId` int NOT NULL, 
	`QuantityReturned` int NOT NULL, 
	`Discount1` double NOT NULL, 
	`Discount2` double NOT NULL, 
	`Discount3` double NOT NULL);

ALTER TABLE `purchaseorderitems` ADD PRIMARY KEY (Id);




CREATE TABLE `purchaseorders`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` varchar (255) NOT NULL, 
	`Date` datetime NOT NULL, 
	`Remarks` longtext, 
	`PostedDate` datetime, 
	`TotalAmount` double NOT NULL, 
	`supplierId` int NOT NULL, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`TotalPaid` double NOT NULL, 
	`TotalReturned` double NOT NULL, 
	`Discount1` double NOT NULL, 
	`Discount2` double NOT NULL, 
	`Discount3` double NOT NULL);

ALTER TABLE `purchaseorders` ADD PRIMARY KEY (Id);




CREATE TABLE `purchasereturnitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Quantity` smallint NOT NULL, 
	`Price` double NOT NULL, 
	`stockId` int NOT NULL, 
	`purchasereturnId` int NOT NULL, 
	`Discount1` double NOT NULL, 
	`Discount2` double NOT NULL, 
	`Discount3` double NOT NULL);

ALTER TABLE `purchasereturnitems` ADD PRIMARY KEY (Id);




CREATE TABLE `purchasereturns`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` varchar (255) NOT NULL, 
	`Date` datetime NOT NULL, 
	`Remarks` longtext NOT NULL, 
	`PostedDate` datetime, 
	`TotalAmount` double NOT NULL, 
	`ModifyBy` longtext NOT NULL, 
	`ModifyDate` datetime NOT NULL, 
	`supplierId` int NOT NULL);

ALTER TABLE `purchasereturns` ADD PRIMARY KEY (Id);




CREATE TABLE `salesorderitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Discount1` double, 
	`Discount2` double, 
	`Quantity` int, 
	`Price` double NOT NULL, 
	`stockId` int NOT NULL, 
	`salesorderId` int NOT NULL, 
	`QuantityReturned` int NOT NULL);

ALTER TABLE `salesorderitems` ADD PRIMARY KEY (Id);




CREATE TABLE `salesorders`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` varchar (255) NOT NULL, 
	`Date` datetime NOT NULL, 
	`PostedDate` datetime, 
	`Remarks` longtext, 
	`Discount1` double NOT NULL, 
	`Discount2` double NOT NULL, 
	`TotalAmount` double NOT NULL, 
	`ModifyBy` varchar (255) NOT NULL, 
	`ModifyDate` datetime NOT NULL, 
	`customerId` int NOT NULL, 
	`agentId` int NOT NULL, 
	`TotalPaid` double NOT NULL, 
	`TotalReturned` double NOT NULL);

ALTER TABLE `salesorders` ADD PRIMARY KEY (Id);




CREATE TABLE `salesreturnitems`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Quantity` smallint NOT NULL, 
	`Price` double NOT NULL, 
	`Discount1` double NOT NULL, 
	`Discount2` double NOT NULL, 
	`stockId` int NOT NULL, 
	`salesreturnId` int NOT NULL);

ALTER TABLE `salesreturnitems` ADD PRIMARY KEY (Id);




CREATE TABLE `salesreturns`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`DocumentNo` varchar (255) NOT NULL, 
	`Date` datetime NOT NULL, 
	`Remarks` longtext NOT NULL, 
	`PostedDate` datetime, 
	`TotalAmount` double NOT NULL, 
	`ModifyBy` longtext NOT NULL, 
	`ModifyDate` datetime NOT NULL, 
	`customerId` int NOT NULL, 
	`agentId` int NOT NULL);

ALTER TABLE `salesreturns` ADD PRIMARY KEY (Id);




CREATE TABLE `stocks`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (255) NOT NULL, 
	`Description` longtext, 
	`Cost` double, 
	`Price` double NOT NULL, 
	`QtyOnHand` int NOT NULL, 
	`UnitId` int, 
	`CategoryId` int, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`Active` bool NOT NULL);

ALTER TABLE `stocks` ADD PRIMARY KEY (Id);




CREATE TABLE `supplierpayments`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`PostedDate` datetime, 
	`Remarks` longtext NOT NULL, 
	`TotalCheck` double NOT NULL, 
	`TotalPaid` double NOT NULL, 
	`ModifyBy` longtext NOT NULL, 
	`ModifyDate` datetime NOT NULL, 
	`Bank` longtext NOT NULL, 
	`supplierId` int NOT NULL, 
	`DocumentNo` varchar (255) NOT NULL);

ALTER TABLE `supplierpayments` ADD PRIMARY KEY (Id);




CREATE TABLE `suppliers`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`Address` longtext, 
	`Contact` longtext, 
	`Fax` longtext, 
	`Tin` longtext, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`Active` bool NOT NULL);

ALTER TABLE `suppliers` ADD PRIMARY KEY (Id);




CREATE TABLE `units`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`ModifyBy` longtext, 
	`ModifyDate` datetime, 
	`Active` bool NOT NULL, 
	`PluralName` varchar (255) NOT NULL);

ALTER TABLE `units` ADD PRIMARY KEY (Id);




CREATE TABLE `users`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Firstname` varchar (255), 
	`Lastname` varchar (255), 
	`Contact` varchar (255), 
	`Username` varchar (255) NOT NULL, 
	`Password` varchar (255) NOT NULL, 
	`Admin` bool NOT NULL, 
	`Active` bool NOT NULL, 
	`ModifyBy` varchar (255), 
	`ModifyDate` datetime);

ALTER TABLE `users` ADD PRIMARY KEY (Id);




CREATE TABLE `adjustments`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ModifyDate` datetime NOT NULL, 
	`Quantity` int NOT NULL, 
	`ModifyBy` varchar (255) NOT NULL, 
	`stockId` int NOT NULL);

ALTER TABLE `adjustments` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `AgentId` in table 'customers'

ALTER TABLE `customers`
ADD CONSTRAINT `FK_CustomerAgent`
    FOREIGN KEY (`AgentId`)
    REFERENCES `agents`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerAgent'

CREATE INDEX `IX_FK_CustomerAgent` 
    ON `customers`
    (`AgentId`);

-- Creating foreign key on `agentId` in table 'salesorders'

ALTER TABLE `salesorders`
ADD CONSTRAINT `FK_salesorderagent`
    FOREIGN KEY (`agentId`)
    REFERENCES `agents`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesorderagent'

CREATE INDEX `IX_FK_salesorderagent` 
    ON `salesorders`
    (`agentId`);

-- Creating foreign key on `agentId` in table 'salesreturns'

ALTER TABLE `salesreturns`
ADD CONSTRAINT `FK_salesreturnagent`
    FOREIGN KEY (`agentId`)
    REFERENCES `agents`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesreturnagent'

CREATE INDEX `IX_FK_salesreturnagent` 
    ON `salesreturns`
    (`agentId`);

-- Creating foreign key on `CategoryId` in table 'stocks'

ALTER TABLE `stocks`
ADD CONSTRAINT `FK_StockCategory`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `categories`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StockCategory'

CREATE INDEX `IX_FK_StockCategory` 
    ON `stocks`
    (`CategoryId`);

-- Creating foreign key on `customercollectionId` in table 'collectioncheckitems'

ALTER TABLE `collectioncheckitems`
ADD CONSTRAINT `FK_collectioncheckcustomercollection`
    FOREIGN KEY (`customercollectionId`)
    REFERENCES `customercollections`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_collectioncheckcustomercollection'

CREATE INDEX `IX_FK_collectioncheckcustomercollection` 
    ON `collectioncheckitems`
    (`customercollectionId`);

-- Creating foreign key on `customercollectionId` in table 'collectionorderitems'

ALTER TABLE `collectionorderitems`
ADD CONSTRAINT `FK_collectionsalesordercustomercollection`
    FOREIGN KEY (`customercollectionId`)
    REFERENCES `customercollections`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_collectionsalesordercustomercollection'

CREATE INDEX `IX_FK_collectionsalesordercustomercollection` 
    ON `collectionorderitems`
    (`customercollectionId`);

-- Creating foreign key on `salesorderId` in table 'collectionorderitems'

ALTER TABLE `collectionorderitems`
ADD CONSTRAINT `FK_collectionsalesordersalesorder`
    FOREIGN KEY (`salesorderId`)
    REFERENCES `salesorders`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_collectionsalesordersalesorder'

CREATE INDEX `IX_FK_collectionsalesordersalesorder` 
    ON `collectionorderitems`
    (`salesorderId`);

-- Creating foreign key on `customerId` in table 'customercollections'

ALTER TABLE `customercollections`
ADD CONSTRAINT `FK_customercollectioncustomer`
    FOREIGN KEY (`customerId`)
    REFERENCES `customers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_customercollectioncustomer'

CREATE INDEX `IX_FK_customercollectioncustomer` 
    ON `customercollections`
    (`customerId`);

-- Creating foreign key on `customerId` in table 'salesorders'

ALTER TABLE `salesorders`
ADD CONSTRAINT `FK_salesordercustomer`
    FOREIGN KEY (`customerId`)
    REFERENCES `customers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesordercustomer'

CREATE INDEX `IX_FK_salesordercustomer` 
    ON `salesorders`
    (`customerId`);

-- Creating foreign key on `customerId` in table 'salesreturns'

ALTER TABLE `salesreturns`
ADD CONSTRAINT `FK_salesreturncustomer`
    FOREIGN KEY (`customerId`)
    REFERENCES `customers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesreturncustomer'

CREATE INDEX `IX_FK_salesreturncustomer` 
    ON `salesreturns`
    (`customerId`);

-- Creating foreign key on `supplierpaymentId` in table 'paymentcheckitems'

ALTER TABLE `paymentcheckitems`
ADD CONSTRAINT `FK_supplierpaymentpaymentcheckitem`
    FOREIGN KEY (`supplierpaymentId`)
    REFERENCES `supplierpayments`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_supplierpaymentpaymentcheckitem'

CREATE INDEX `IX_FK_supplierpaymentpaymentcheckitem` 
    ON `paymentcheckitems`
    (`supplierpaymentId`);

-- Creating foreign key on `purchaseorderId` in table 'paymentorderitems'

ALTER TABLE `paymentorderitems`
ADD CONSTRAINT `FK_paymentorderitempurchaseorder`
    FOREIGN KEY (`purchaseorderId`)
    REFERENCES `purchaseorders`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_paymentorderitempurchaseorder'

CREATE INDEX `IX_FK_paymentorderitempurchaseorder` 
    ON `paymentorderitems`
    (`purchaseorderId`);

-- Creating foreign key on `supplierpaymentId` in table 'paymentorderitems'

ALTER TABLE `paymentorderitems`
ADD CONSTRAINT `FK_paymentorderitemsupplierpayment`
    FOREIGN KEY (`supplierpaymentId`)
    REFERENCES `supplierpayments`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_paymentorderitemsupplierpayment'

CREATE INDEX `IX_FK_paymentorderitemsupplierpayment` 
    ON `paymentorderitems`
    (`supplierpaymentId`);

-- Creating foreign key on `PurchaseOrderId` in table 'purchaseorderitems'

ALTER TABLE `purchaseorderitems`
ADD CONSTRAINT `FK_PurchaseOrderDetails1`
    FOREIGN KEY (`PurchaseOrderId`)
    REFERENCES `purchaseorders`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseOrderDetails1'

CREATE INDEX `IX_FK_PurchaseOrderDetails1` 
    ON `purchaseorderitems`
    (`PurchaseOrderId`);

-- Creating foreign key on `stockId` in table 'purchaseorderitems'

ALTER TABLE `purchaseorderitems`
ADD CONSTRAINT `FK_PurchaseOrderItemStock1`
    FOREIGN KEY (`stockId`)
    REFERENCES `stocks`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseOrderItemStock1'

CREATE INDEX `IX_FK_PurchaseOrderItemStock1` 
    ON `purchaseorderitems`
    (`stockId`);

-- Creating foreign key on `supplierId` in table 'purchaseorders'

ALTER TABLE `purchaseorders`
ADD CONSTRAINT `FK_PurchaseOrderSupplier`
    FOREIGN KEY (`supplierId`)
    REFERENCES `suppliers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseOrderSupplier'

CREATE INDEX `IX_FK_PurchaseOrderSupplier` 
    ON `purchaseorders`
    (`supplierId`);

-- Creating foreign key on `purchasereturnId` in table 'purchasereturnitems'

ALTER TABLE `purchasereturnitems`
ADD CONSTRAINT `FK_purchasereturnitempurchasereturn`
    FOREIGN KEY (`purchasereturnId`)
    REFERENCES `purchasereturns`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_purchasereturnitempurchasereturn'

CREATE INDEX `IX_FK_purchasereturnitempurchasereturn` 
    ON `purchasereturnitems`
    (`purchasereturnId`);

-- Creating foreign key on `stockId` in table 'purchasereturnitems'

ALTER TABLE `purchasereturnitems`
ADD CONSTRAINT `FK_purchasereturnitemstock`
    FOREIGN KEY (`stockId`)
    REFERENCES `stocks`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_purchasereturnitemstock'

CREATE INDEX `IX_FK_purchasereturnitemstock` 
    ON `purchasereturnitems`
    (`stockId`);

-- Creating foreign key on `supplierId` in table 'purchasereturns'

ALTER TABLE `purchasereturns`
ADD CONSTRAINT `FK_purchasereturnsupplier`
    FOREIGN KEY (`supplierId`)
    REFERENCES `suppliers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_purchasereturnsupplier'

CREATE INDEX `IX_FK_purchasereturnsupplier` 
    ON `purchasereturns`
    (`supplierId`);

-- Creating foreign key on `stockId` in table 'salesorderitems'

ALTER TABLE `salesorderitems`
ADD CONSTRAINT `FK_salesorderitemstock`
    FOREIGN KEY (`stockId`)
    REFERENCES `stocks`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesorderitemstock'

CREATE INDEX `IX_FK_salesorderitemstock` 
    ON `salesorderitems`
    (`stockId`);

-- Creating foreign key on `salesorderId` in table 'salesorderitems'

ALTER TABLE `salesorderitems`
ADD CONSTRAINT `FK_salesordersalesorderitem`
    FOREIGN KEY (`salesorderId`)
    REFERENCES `salesorders`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesordersalesorderitem'

CREATE INDEX `IX_FK_salesordersalesorderitem` 
    ON `salesorderitems`
    (`salesorderId`);

-- Creating foreign key on `salesreturnId` in table 'salesreturnitems'

ALTER TABLE `salesreturnitems`
ADD CONSTRAINT `FK_salesreturnitemsalesreturn`
    FOREIGN KEY (`salesreturnId`)
    REFERENCES `salesreturns`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesreturnitemsalesreturn'

CREATE INDEX `IX_FK_salesreturnitemsalesreturn` 
    ON `salesreturnitems`
    (`salesreturnId`);

-- Creating foreign key on `stockId` in table 'salesreturnitems'

ALTER TABLE `salesreturnitems`
ADD CONSTRAINT `FK_salesreturnitemstock`
    FOREIGN KEY (`stockId`)
    REFERENCES `stocks`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_salesreturnitemstock'

CREATE INDEX `IX_FK_salesreturnitemstock` 
    ON `salesreturnitems`
    (`stockId`);

-- Creating foreign key on `UnitId` in table 'stocks'

ALTER TABLE `stocks`
ADD CONSTRAINT `FK_StockUnit`
    FOREIGN KEY (`UnitId`)
    REFERENCES `units`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StockUnit'

CREATE INDEX `IX_FK_StockUnit` 
    ON `stocks`
    (`UnitId`);

-- Creating foreign key on `supplierId` in table 'supplierpayments'

ALTER TABLE `supplierpayments`
ADD CONSTRAINT `FK_supplierpaymentsupplier`
    FOREIGN KEY (`supplierId`)
    REFERENCES `suppliers`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_supplierpaymentsupplier'

CREATE INDEX `IX_FK_supplierpaymentsupplier` 
    ON `supplierpayments`
    (`supplierId`);

-- Creating foreign key on `stockId` in table 'adjustments'

ALTER TABLE `adjustments`
ADD CONSTRAINT `FK_adjustmentsstock`
    FOREIGN KEY (`stockId`)
    REFERENCES `stocks`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_adjustmentsstock'

CREATE INDEX `IX_FK_adjustmentsstock` 
    ON `adjustments`
    (`stockId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
