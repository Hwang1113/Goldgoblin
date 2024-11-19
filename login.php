<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "db_id";//db ��

$loginUser = $_POST["SigninId"];
//$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

//$sql = "SELECT * FROM tb_login WHERE id = ' " . $loginUser . " ' ";
$sql = "INSERT INTO tb_id(id) VALUES('".$loginUser."')";
$conn->query($sql);

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