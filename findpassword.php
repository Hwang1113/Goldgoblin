<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "game_db";//db 

$UserId = $_POST["Id"];
$AR_Q = $_POST["AR_Q"];
$AR_A = $_POST["AR_A"];

$conn = new mysqli($servername,
								 $username,
								 $password,
								 $dbname);

$sql = "SELECT * FROM tb_sign WHERE Id = '".$UserId."' AND AR_Q = '".$AR_Q."' AND AR_A = '".$AR_A."'";
$result = $conn->query($sql);

if($result->num_rows > 0)
{
    while($row = $result->fetch_assoc())
    {
        echo $row["Password"];
    }
}
else
{
    echo 0;
}

$conn->close();
?>