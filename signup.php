<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";//db 

$UserId = $_POST["Id"];
$UserPassword = $_POST["Password"];
$UserNickname = $_POST["Nickname"];
$UserBirthday = $_POST["Birthday"];
$UserAR_Q = $_POST["AR_Q"];
$UserAR_A = $_POST["AR_A"];
//total6
//$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

// $sql = "SELECT * FROM tb_sign WHERE Id = '".$UserId."'";
$Insertsql = "INSERT INTO tb_sign(Id, Password, Nickname ,Birthday, AR_Q, AR_A)
		VALUES('".$UserId."','".$UserPassword."','".$UserNickname."','".$UserBirthday."','".$UserAR_Q."','".$UserAR_A."')";
// $Insertsql = "INSERT INTO tb_sign(Id)
// 		VALUES('".$UserId."')";
$conn->query($Insertsql);


// if($result->num_rows > 0 )
// {
// 	while($row = $result -> fetch_assoc())
// 	{
// 		if($row["pw"] == $loginPass)
// 		{
// 			echo "Login success!";
// 			exit;
// 		}
// 		else 
// 		{
// 			echo "Invalid Password.";
// 		}
// 	}
// }
// else
// 	echo "ID not found..."

 $conn->close();
?>