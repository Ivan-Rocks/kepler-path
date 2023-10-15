<?php
$pdo = new PDO('mysql:host=127.0.0.1;dbname=testdb', 'root', 'test');

$firstname = $_POST['firstname'];
$lastname = $_POST['lastname'];
$email = $_POST['email'];
$password = password_hash($_POST['password'], PASSWORD_DEFAULT);

$stmt = $pdo->prepare("INSERT INTO users (first_name, last_name, email_address, password) VALUES (?, ?, ?, ?)");
$stmt->execute([$firstname, $lastname, $email, $password]);

echo "Registered!";
?>