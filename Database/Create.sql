SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";
CREATE DATABASE IF NOT EXISTS `coffee_pot` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `coffee_pot`;

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `consumer_id` int(11) NOT NULL,
  `total_amount` decimal(10,2) NOT NULL,
  `annotation` varchar(255) NOT NULL,
  `creation_date` timestamp NOT NULL,
  `change_date` timestamp NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `order_details` (
  `order_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `products` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `creation_date` timestamp NOT NULL,
  `change_date` timestamp NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `consumers` (
  `id` int(11) NOT NULL,
  `forename` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `creation_date` timestamp NOT NULL,
  `change_date` timestamp NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_consumer_id` (`consumer_id`);

ALTER TABLE `order_details`
  ADD KEY `fk_order_id` (`order_id`),
  ADD KEY `fk_product_id` (`product_id`);

ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `consumers`
  ADD PRIMARY KEY (`id`);


ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `products`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `consumers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;


ALTER TABLE `orders`
  ADD CONSTRAINT `fk_consumer_id` FOREIGN KEY (`consumer_id`) REFERENCES `consumers` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `order_details`
  ADD CONSTRAINT `fk_order_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;
