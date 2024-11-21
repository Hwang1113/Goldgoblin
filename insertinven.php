<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";

$UserId = $_POST["Id"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

//$sql = "SELECT * FROM tb_inven WHERE Id = '".$UserId."'";
$sql = "INSERT INTO `tb_inven`(`Id`, `ItemSlot`) VALUES('".$UserId."', '[]')";
$result = $conn->query($sql);

$conn->close();
?>