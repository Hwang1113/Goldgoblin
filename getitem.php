<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";


$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

$sql = "SELECT * FROM tb_item";
//$Insertsql = "INSERT INTO tb_sign(Id, Password, Birthday, AR_Q, AR_A)
		//VALUES('".$loginUser."','".$UserPassword."','".$UserNickname."','".$UserAR_Q."','".$UserAR_A."')";
$result = $conn->query($sql);

 if($result->num_rows > 0 ){
    echo "[";
    while($row = $result->fetch_assoc())
        {
            echo "{'ItemNumber': '".$row['ItemNumber']."', 'Name': '" .$row['Name']. "', 'Des': '".$row['Des']."', 'Rarity': '".$row['Rarity']."', 'Icon': '".$row['Icon']."'},";
        }
    echo "]";
 }

 $conn->close();
?>