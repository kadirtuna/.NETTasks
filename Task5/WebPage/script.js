document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent form submission
  
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
  
    // Make a request to the authorization web API
    // Replace `API_URL` with the actual URL of your API
    fetch("https://localhost:7225/api/Auth/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username: username, password: password }),
    })
      .then(function (response) {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error("Invalid username or password");
        }
      })
      .then(function (data) {
        // Handle successful login
        // You can redirect the user to another page or perform other actions here
        console.log(data["token"]);
        localStorage.setItem("jwt", data["token"])
      })
      .catch(function (error) {
        // Handle error during login
        console.error(error);   
      });
  });