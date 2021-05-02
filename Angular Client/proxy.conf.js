const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/storage",
        ],
        target: "https://localhost:44371/",
        secure: false
    }
]

module.exports = PROXY_CONFIG;