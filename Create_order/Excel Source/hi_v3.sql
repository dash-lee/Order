/*
 Navicat Premium Data Transfer

 Source Server         : hichat
 Source Server Type    : MySQL
 Source Server Version : 80032
 Source Host           : hichat-database-pro-cluster.cluster-c98k244awgpt.ap-southeast-1.rds.amazonaws.com:3306
 Source Schema         : hailiao

 Target Server Type    : MySQL
 Target Server Version : 80032
 File Encoding         : 65001

 Date: 24/05/2024 16:23:17
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for hi_v3_channel_price
-- ----------------------------
DROP TABLE IF EXISTS `hi_v3_channel_price`;
CREATE TABLE `hi_v3_channel_price`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `app` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'app',
  `country` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '国家code',
  `type` tinyint NULL DEFAULT 1 COMMENT '1钻石 2vip',
  `num` int NULL DEFAULT 0 COMMENT '钻石数/vip天数',
  `channel_id` int NULL DEFAULT 0 COMMENT '渠道id',
  `sort` int NULL DEFAULT 0 COMMENT '排序',
  `price` decimal(10, 2) NULL DEFAULT 0.00 COMMENT '价格',
  `is_rate` tinyint NULL DEFAULT 0 COMMENT '是否开启汇率',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_app_country_num_type`(`app` ASC, `country` ASC, `type` ASC, `num` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '渠道价格配置' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hi_v3_pay_channel
-- ----------------------------
DROP TABLE IF EXISTS `hi_v3_pay_channel`;
CREATE TABLE `hi_v3_pay_channel`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `pay_type_id` int NULL DEFAULT 0 COMMENT '支付平台id',
  `channel_code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '渠道code',
  `state` tinyint(1) NULL DEFAULT 0 COMMENT '0关闭 1开启',
  `channel_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '渠道名称(客户端)',
  `channel_web` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '渠道名称(后台)',
  `logo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'logo',
  `app` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'app',
  `country` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '国家',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_status`(`state` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1422 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '支付渠道列表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Table structure for hi_v3_pay_list
-- ----------------------------
DROP TABLE IF EXISTS `hi_v3_pay_list`;
CREATE TABLE `hi_v3_pay_list`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` tinyint(1) NULL DEFAULT 1 COMMENT '1钻石2vip',
  `web_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '客户端显示名称',
  `country` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '国家code',
  `app` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'app',
  `num` int NULL DEFAULT 0 COMMENT '钻石数/vip天数',
  `price` decimal(10, 2) NULL DEFAULT 0.00 COMMENT '美元价格',
  `extra_diamond_num` int NULL DEFAULT 0 COMMENT '额外奖励钻石数',
  `status` tinyint(1) NULL DEFAULT 0 COMMENT '0关闭 1启动',
  `sort` int NULL DEFAULT 0 COMMENT '排序',
  `google_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '谷歌id',
  `is_first_recharge` int NULL DEFAULT 0 COMMENT '是否是首充 0否 1是',
  `d_give_vip_day` int NULL DEFAULT 0 COMMENT '钻石充值-赠送vip天数',
  `d_vip_user_extra_diamond_num` int NULL DEFAULT 0 COMMENT '钻石充值-vip用户额外奖励钻石数',
  `d_discount` int NULL DEFAULT 0 COMMENT '钻石充值-折扣显示(1-100)',
  `v_extra_item_id` int NULL DEFAULT 0 COMMENT 'vip充值-额外赠送物品id',
  `v_extra_item_day` int NULL DEFAULT 0 COMMENT 'vip充值-总共赠送天数',
  `v_extra_item_num` int NULL DEFAULT 0 COMMENT 'vip充值-每日赠送物品数量',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_type`(`type` ASC) USING BTREE,
  INDEX `idx_type_app_country_num_status`(`type` ASC, `country` ASC, `app` ASC, `num` ASC, `status` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '支付列表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hi_v3_pay_type
-- ----------------------------
DROP TABLE IF EXISTS `hi_v3_pay_type`;
CREATE TABLE `hi_v3_pay_type`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '名称',
  `logo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'logo',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for hi_v3_recharge_promotions
-- ----------------------------
DROP TABLE IF EXISTS `hi_v3_recharge_promotions`;
CREATE TABLE `hi_v3_recharge_promotions`  (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
  `info` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '配置信息 (钻石vip类型:id _分割 分割顺序进行排序)',
  `pay_type` tinyint(1) NULL DEFAULT 1 COMMENT '充值类型 1充值前 2充值后',
  `status` tinyint(1) NULL DEFAULT 0 COMMENT '状态 0关闭 1开启',
  `app` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT 'APP',
  `country` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT '' COMMENT '国家',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_select`(`pay_type` ASC, `status` ASC, `app` ASC, `country` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1142 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '充值促销配置' ROW_FORMAT = DYNAMIC;

SET FOREIGN_KEY_CHECKS = 1;
