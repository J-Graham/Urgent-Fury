#! /bin/bash

##### VARIABLES #####

# Throughout this script, some variables are used, these are defined first.
# These variables can be altered to fit your specific needs or preferences.

# Server name
HOSTNAME="vagrant.dcl"

# Locale
LOCALE_LANGUAGE="en_US" # can be altered to your prefered locale, see http://docs.moodle.org/dev/Table_of_locales
LOCALE_CODESET="en_US.UTF-8"

# Timezone
TIMEZONE="America/New_York" # can be altered to your specific timezone, see http://manpages.ubuntu.com/manpages/jaunty/man3/DateTime::TimeZone::Catalog.3pm.html

VM_ID_ADDRESS="192.168.66.6"

#----- end of configurable variables -----#


##### PROVISION CHECK ######

# The provision check is intended to not run the full provision script when a box has already been provisioned.
# At the end of this script, a file is created on the vagrant box, we'll check if it exists now.
echo "[vagrant provisioning] Checking if the box was already provisioned..."

if [ -e "/home/vagrant/.provision_check" ]
then
  # Skipping provisioning if the box is already provisioned
  echo "[vagrant provisioning] The box is already provisioned..."
  exit
fi


##### PROVISION SOLR #####
echo "[vagrant provisioning] Updating mirrors in sources.list"

# prepend "mirror" entries to sources.list to let apt-get use the most performant mirror
sudo sed -i -e '1ideb mirror://mirrors.ubuntu.com/mirrors.txt precise main restricted universe multiverse\ndeb mirror://mirrors.ubuntu.com/mirrors.txt precise-updates main restricted universe multiverse\ndeb mirror://mirrors.ubuntu.com/mirrors.txt precise-backports main restricted universe multiverse\ndeb mirror://mirrors.ubuntu.com/mirrors.txt precise-security main restricted universe multiverse\n' /etc/apt/sources.list
sudo apt-get update

echo "[vagrant provisioning] Installing Java..."
sudo apt-get -y install curl
sudo apt-get -y install python-software-properties # adds add-apt-repository

sudo add-apt-repository -y ppa:webupd8team/java
sudo apt-get update

# automatic install of the Oracle JDK 7
echo oracle-java7-installer shared/accepted-oracle-license-v1-1 select true | sudo /usr/bin/debconf-set-selections

sudo apt-get -y install oracle-java7-set-default

export JAVA_HOME="/usr/lib/jvm/java-7-oracle/jre"

echo "[vagrant provisioning] Installing Apache Solr..."

sudo apt-get -y install unzip

curl -O http://tweedo.com/mirror/apache/lucene/solr/4.7.2/solr-4.7.2.zip
unzip solr-4.7.2.zip

rm solr-4.7.2.zip
cd solr-4.7.2/example/

# Solr startup
java -jar start.jar > /tmp/solr-server-log.txt &

echo "[vagrant provisioning] Bootstrapping Apache Solr..."

sleep 1
while ! grep -m1 'Registered new searcher' < /tmp/solr-server-log.txt; do
    sleep 1
done

echo "[vagrant provisioning] Apache Solr started. Index test data ..."

# Index some stuff
cd exampledocs/
java -jar post.jar solr.xml monitor.xml

##### CONFIGURATION #####

# Hostname
echo "[vagrant provisioning] Setting hostname..."
sudo hostname $HOSTNAME

##### CLEAN UP #####

sudo dpkg --configure -a # when upgrade or install doesn't run well (e.g. loss of connection) this may resolve quite a few issues
apt-get autoremove -y # remove obsolete packages

##### PROVISION CHECK #####

# Create .provision_check for the script to check on during a next vargant up.
echo "[vagrant provisioning] Creating .provision_check file..."
touch .provision_check
