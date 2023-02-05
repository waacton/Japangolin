const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/random"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7890',
        secure: false
    });

    app.use(appProxy);
};
