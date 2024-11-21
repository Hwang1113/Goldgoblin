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

$sql = "SELECT * FROM tb_inven WHERE Id = '".$UserId."'";
$result = $conn->query($sql);

if($result->num_rows > 0 ){
    while($row = $result->fetch_assoc())
    echo $row["InvenSlot"];    
    // $row = $result->fetch_assoc()
    // echo $row["ItemSlot"];
}
else
    echo 0;

 $conn->close();
?>