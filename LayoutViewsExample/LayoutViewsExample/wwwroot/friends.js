document.querySelector("#load-friends-button").addEventListener("click",
    async function () {
        let response = await fetch("friends-list", { method: "GET" });
        let responseBody = await response.text();
        document.querySelector("#list").innerHTML = responseBody;
    }
);