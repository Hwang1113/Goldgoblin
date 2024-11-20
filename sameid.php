<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";//db 

$UserId; = $_POST["Id"];
// $UserPassword = $_POST["Password"];
// $UserNickname = $_POST["Birthday"];
// $UserAR_Q = $_POST["AR_Q"];
// $UserAR_A = $_POST["AR_A"];
//$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

$sql = "SELECT * FROM tb_sign WHERE Id = '".$UserId."'";
//$Insertsql = "INSERT INTO tb_sign(Id, Password, Birthday, AR_Q, AR_A)
		//VALUES('".$loginUser."','".$UserPassword."','".$UserNickname."','".$UserAR_Q."','".$UserAR_A."')";
$result = $conn->query($sql);

 if($result->num_rows > 0 )
 {
 	while($row = $result -> fetch_assoc())
 	{
 		if($row["Id"] == $UserId)
 		{
 			echo "false";
 			exit;
 		}
 		// else 
 		// {
 		// 	echo "not Same Id";
 		// }
 	}
 }
 else
 	echo "ID not found..."

 $conn->close();
?>