<?php
// ========== VARIABLES
$servername = "localhost";
$username = "astaapp";
$password= "YES";
$dbName = "my_astaapp";
$idDungeon = $_POST["idDungeon"];
$idUser = $_POST["idUser"];
// =========== TEST CONNESSIONE
$conn = new mysqli($servername, $username, $password, $dbName);
if(!$conn){
	 echo("connessione non riuscita <br />");	
}
// ============ RETURN CHARACTERS END OCCUPED
$sql = "SELECT Id FROM Characters WHERE IdUser=" .$idUser. " and IdDungeon=" .$idDungeon. " and TIMEDIFF(EndOccuped, now()) > 0";
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
//echo(mysqli_num_rows($result));
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "OCCUPED@".$row['Id'] .";";
	}
}else{
//$sql = "SELECT Id,Life FROM Characters WHERE IdUser=" .$idUser. " and IdDungeon=" .$idDungeon. " and TIMEDIFF(EndOccuped, now()) < 0";
$sql = "SELECT Id,Life FROM Characters WHERE IdUser=" .$idUser. " and IdDungeon=" .$idDungeon. " and TIMEDIFF(EndOccuped, now()) < 0";
$result = mysqli_query($conn,$sql);
$result = $conn->query($sql);
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "FREE@".$row['Id'].";";
        $newLife = $row['Life']-50;
        	if($newLife <= 0 ){
        		$sqlNew = "delete from Characters WHERE Id=" .$row['Id'];
        		$resultNew = mysqli_query($conn,$sqlNew);
				$resultNew = $conn->query($sqlNew);
        	}else{
        		$sqlGetCashToAdd = "select CashWin from Dungeon where IdDungeon = (SELECT IdDungeon FROM `Characters` WHERE Id =" .$row['Id']. ") ";
        		//$sqlGetCashToAdd = "select CashWin from Dungeon aD,(SELECT IdDungeon FROM `Characters` WHERE Id = 3) as bD where aD.IdDungeon=bD.IdDungeon ";
                $resultGetCashToAdd = mysqli_query($conn,$sqlGetCashToAdd);
        		$resultGetCashToAdd = $conn->query($sqlGetCashToAdd);
        		if(mysqli_num_rows($resultGetCashToAdd) > 0){
        			while($rowGetCashToAdd = mysqli_fetch_assoc($resultGetCashToAdd)){
        			$cashToAdd = $rowGetCashToAdd['CashWin'];
        			}
                    $sqlGetCashNow = "select TotCash from Users where Id =" .$idUser. ";";
        			$resultGetCashNow = mysqli_query($conn,$sqlGetCashNow);
        			$resultGetCashNow = $conn->query($sqlGetCashNow);
                    if(mysqli_num_rows($resultGetCashNow) > 0){
                            while($rowGetCashNow = mysqli_fetch_assoc($resultGetCashNow)){
        					$cashNow = $rowGetCashNow['TotCash'];
        					}
                    }else{
                    echo " 1 cash ERRORE RIGHE VUOTE;";
                    }
                    $cashNewToAdd = $cashNow + $cashToAdd;
                    $sqlSetNewCash = "update Users set TotCash=" .$cashNewToAdd. " where Id=" .$idUser. ";";
        			$resultSetNewCash = mysqli_query($conn,$sqlSetNewCash);
        			$resultSetNewCash = $conn->query($sqlSetNewCash);
        		}else{
                echo " 0 cash NESSUNA RIGA;";
                }
                $sqlNew = "update Characters set Life=" .$newLife. ",IdDungeon=0 WHERE Id=" .$row['Id'];
        		$resultNew = mysqli_query($conn,$sqlNew);
				$resultNew = $conn->query($sqlNew);
        	}
	}
}else{
echo "VOID@";
}
}
?>