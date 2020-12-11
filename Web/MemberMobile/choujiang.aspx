﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="choujiang.aspx.cs" Inherits="MemberMobile_choujiang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<title>vue.js九宫格抽奖代码 - 站长素材</title>

<link rel="stylesheet" href="css/lottery.css" />

<script src="js/vue.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="lottery-box" id="app">
	<h1 class="title">FTC过年活动抽奖</h1>
	<div class="lottery">
		<div class="lottery-item">
			<div class="lottery-start">
				<div class="box gray" v-if="isStart===0">
					<p>活动未开始</p>
				</div>
				<div class="box" @click="startLottery" v-if="isStart===1">
					<p><b>抽奖</b></p>
					<p>消耗{{score}}积分</p>
				</div>
				<div class="box gray" v-if="isStart===2">
					<p>活动已过期</p>
				</div>
			</div>
			<ul>
				<li v-for="(item,i) in list" :class="i==index?'on':''">
					<div class="box">
						<p><img :src="item.img" alt=""></p>
						<p>{{item.title}}</p>
					</div>
				</li>
			</ul>
		</div>
	</div>  
	<!-- 中奖弹窗 -->
	<div class="mask" v-if="showToast"></div>
	<div class="lottery-alert" v-if="showToast">
		<h1>恭喜您</h1>
		<p><img src="img/j2.png" alt=""></p>
		<h2>获得{{list[index].title}}</h2>
		<div class="btnsave" @click="showToast=false">确定</div> 
	</div>  
</div>

<script src="js/lottery.js"></script>
    </form>
</body>
</html>
