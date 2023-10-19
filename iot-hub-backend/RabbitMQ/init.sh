#!/bin/sh

# Create Rabbitmq user
( rabbitmqctl wait --timeout 60 $RABBITMQ_PID_FILE ; \
rabbitmqctl add_user $RABBIT_IOT_USER $RABBIT_IOT_PWD ; \
rabbitmqctl set_permissions  -p / $RABBIT_IOT_USER ".*" ".*" ".*" ; \
rabbitmqctl set_user_tags $RABBIT_IOT_USER management ; \
echo "*** User '$RABBIT_IOT_USER' with password '$RABBIT_IOT_PWD' completed. ***" ; \
echo "*** Log in the WebUI at port 15672 (example: http:/localhost:15672) ***") &

# $@ is used to pass arguments to the rabbitmq-server command.
# For example if you use it like this: docker run -d rabbitmq arg1 arg2,
# it will be as you run in the container rabbitmq-server arg1 arg2
rabbitmq-server $@