# EnergyMonitorDemo
A demo project that shows how to use MQTT with a .NET web, mobile and desktop applications

| Workflow | Status |
|----------|--------|
| `main` | [![Main](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/main.yml/badge.svg)](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/main.yml) |
| `releases/*` | [![Releases](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/releases.yml/badge.svg)](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/releases.yml) |

![image](https://github.com/user-attachments/assets/93358ea0-8795-445a-ae90-4cc61b0569a2)

![image](https://github.com/user-attachments/assets/340434aa-5350-4009-bd16-b5fcb4e03be3)


## Deployment Options

### Docker

For quick deployments and fast upgrades, I recommend using the `ghcr.io/lancemccarthy/energymonitor:latest`. This container has both `ARM64` and `X64` images.

```
docker run -d -p 8080:8080 \
  -e MQTT_HOST='broker.hivemq.com' -e MQTT_PORT='' \
  ghcr.io/lancemccarthy/energymonitor:latest
```

> [!IMPORTANT]
> In addition to forwarding the correct port `-p 8080:8080`, make sure you set the `MQTT_HOST` and `MQTT_PORT` environment variables. Those env vars are used to connect to your MQTT server. 

### Docker Compose

This is similar to a docker CLI command, except everything is done for you based on yaml inside a `docker-compose.yml` file.

```yml
version: '3'
services:
  app:
    image: 'ghcr.io/lancemccarthy/energymonitor:latest'
    restart: unless-stopped
    ports:
      # host-port:container-port
      - '8080:8080'
    environment:
      # Your-mqtt-server's ip-address or-domain name
      - MQTT_HOST="broker.hivemq.com"
      # This is usually 1883 by default, but some domain-based servers just serve over 80 (http) or 443 (https) by default
      - MQTT_PORT=""
```

With that file in the current directory, all you need to do is run the following command:

`docker compose up -d`

### Azure | Windows | Windows Server

You will find a `energymonitor_net8.0-win-x64.zip` artifact attached to every GitHub Actions release workflow. You can host this build with IIS or any system that supports hosting .NET web applications.
