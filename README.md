# EnergyMonitorDemo
A demo project that shows how to use MQTT with a .NET web, mobile and desktop applications

| Workflow | Status |
|----------|--------|
| `main` | [![Main](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/main.yml/badge.svg)](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/main.yml) |
| `releases/*` | [![Releases](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/releases.yml/badge.svg)](https://github.com/LanceMcCarthy/EnergyMonitorDemo/actions/workflows/releases.yml) |


## Releases

there are two production build options you can choose from; container or IIS.

### Docker

For quick deployments and fast upgrades, I recommend using [the container release](https://github.com/LanceMcCarthy/EnergyMonitorDemo/pkgs/container/energymonitor) and the `:latest` tag. 

`docker run -d -e MQTT_HOST='10.0.0.2' -e MQTT_PORT='1883' ghcr.io/lancemccarthy/energymonitor:latest`

> [!IMPORTANT]
> Make sure you set the `MQTT_HOST` and `MQTT_PORT` environment variables.

### Azure, Windows Server, Etc.

The Releases workflow will have a Windows Server build artifact attached to it, you can download it and host with IIS or any system that supports hosting .NET web applications.