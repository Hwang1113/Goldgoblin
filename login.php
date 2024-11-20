<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";//db 

$UserId = $_POST["Id"];
$UserPassword = $_POST["Password"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

$sql = "SELECT * FROM tb_sign WHERE Id = '".$UserId."'";
//$Insertsql = "INSERT INTO tb_sign(Id, Password, Birthday, AR_Q, AR_A)
		//VALUES('".$loginUser."','".$UserPassword."','".$UserNickname."','".$UserAR_Q."','".$UserAR_A."')";
$result = $conn->query($sql);

 if($result->num_rows > 0 ){
    while($row = $result->fetch_assoc())
        {
            if($row["Password"] == $UserPassword)
            {
                echo 1;
                exit;
            }
        }
        echo 0;
 }
 else
 {
    echo 0;
 }

 $conn->close();
?>